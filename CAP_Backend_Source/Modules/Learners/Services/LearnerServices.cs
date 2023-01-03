﻿using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Learners.Requests;
using CAP_Backend_Source.Modules.Programs.Service;
using Infrastructure.Exceptions.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace CAP_Backend_Source.Modules.Learners.Services
{
    public interface ILearnerServices
    {
        Task RegisterOrUnRegisterAsync(int userId, RegisterOrUnRegisterRequest request);
        Task ImportAsync(ImportLearnerRequest request);
        Task<List<Learner>> GetListLearners(int idProgram);
        Task<Learner> AddLearner(AddLearnerRequest request);
    }

    public class LearnerServices : ILearnerServices
    {
        private MyDbContext _myDbContext;
        private readonly IFileStorageService _fileStorageService;

        public LearnerServices(MyDbContext myDbContext, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _myDbContext = myDbContext;
        }
        public async Task RegisterOrUnRegisterAsync(int userId, RegisterOrUnRegisterRequest request)
        {
            var leaner = _myDbContext.Learners.Where(x => x.AccountIdLearner == userId && x.ProgramId == request.ProgramId).FirstOrDefault();
            if (request.IsRegister && leaner == null)
            {
                await _myDbContext.Learners.AddAsync(new Learner()
                {
                    ProgramId = request.ProgramId,
                    AccountIdLearner = userId,
                    RegisterStatus = "UnApproved",
                    IsRegister = true,

                });
            }
            else if (!request.IsRegister && leaner != null)
            {
                _myDbContext.Learners.Remove(leaner);
            }
            _myDbContext.SaveChanges();
        }

        public async Task ImportAsync(ImportLearnerRequest request)
        {

            Models.Program? program = await _myDbContext.Programs.Where(x => x.ProgramId == request.ProgramId)
                .FirstOrDefaultAsync() ?? throw new BadRequestException("ProgramId Not Found");
            request.Emails!.ForEach(x => x.ToLower());
            var accs = _myDbContext.Accounts.Where(x => request.Emails.Contains(x.Email.ToLower()));

            if (accs.Count() != request.Emails.Count)
            {
                throw new BadRequestException("Email Invalid");
            }
            await _myDbContext.Learners.AddRangeAsync(request.Emails!.Select(x => new Learner()
            {
                ProgramId = request.ProgramId,
                AccountIdLearner = accs.First(y => y.Email.ToLower() == x.ToLower()).AccountId,
                RegisterStatus = "Approved",
                IsRegister = false,

            }));
            _myDbContext.SaveChanges();

        }

        public async Task<List<Learner>> GetListLearners(int idProgram)
        {
            List<Learner> _listLearner = await _myDbContext.Learners.Where(l => l.IsRegister == false && l.ProgramId == idProgram).ToListAsync();
            if(_listLearner == null)
            {
                throw new BadRequestException("Couldn't find a list of learner");
            }
            return _listLearner;
        }

        public async Task<Learner> AddLearner(AddLearnerRequest request)
        {
            var checkLearner = await _myDbContext.Learners.FirstOrDefaultAsync(l => l.AccountIdLearner == request.AccountIdLearner && l.ProgramId == request.ProgramId);
            if (checkLearner != null) 
            {
                throw new BadRequestException("Learners already exist in this program.");
            }

            var _learner = new Learner
            {
                AccountIdLearner = request.AccountIdLearner,
                ProgramId = request.ProgramId,
                AccountIdApprover = request.AccountIdApprover,
                Status = "Đang tham gia",
                IsRegister = false,
            };

            await _myDbContext.Learners.AddAsync(_learner);
            await _myDbContext.SaveChangesAsync();
            return _learner;
        }
    }
}
