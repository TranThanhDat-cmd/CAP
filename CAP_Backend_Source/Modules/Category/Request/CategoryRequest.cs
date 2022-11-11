namespace CAP_Backend_Source.Modules.Category.Request
{
    public class CategoryRequest
    {
        public class CreateCategoryRequest
        {
            public string Name { get; set; }
        }
        public class EditCategoryRequest
        {
            public string Name { get; set; }
        }
    }
}
