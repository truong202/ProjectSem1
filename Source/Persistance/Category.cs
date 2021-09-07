using System;

namespace Persistance
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                Category category = (Category)obj;
                return
                // this.CategoryId.Equals(category.CategoryId) &&
                this.CategoryName.Equals(category.CategoryName) ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.CategoryId.GetHashCode();
        }
    }
}