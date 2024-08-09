using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalItem { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }


        public PaginatedList(List<T> items,int count,int pageindex,int pagesize ) 
        {
            PageIndex = pageindex;
            TotalPage = (int)Math.Ceiling(count / (double)pagesize);
            Items = items;
        }

        public bool HasPreviousPage => (PageIndex == 1) ? true : false;
        public bool HasNextPage => (PageIndex >= TotalPage) ? true : false;

        public static PaginatedList<T> CreateAsync(List<T> source, int pageindex,int pagesize)
        {
            var count = source.Count(); // tổng số hàng hóa
            var items = source.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(items,count,pageindex,pagesize);

        }
    }
}
