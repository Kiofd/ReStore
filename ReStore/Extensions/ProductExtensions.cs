using Re_Store.Entities;

namespace Re_Store.Extensions;

public static class ProductExtensions
{
   public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
   {
      if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(p => p.Name);
      
      query = orderBy switch
      {
         "price" => query.OrderBy(p => p.Price), //ascending
         "priceDecs" => query.OrderByDescending(p => p.Price), //descending
         _ => query.OrderBy(p => p.Name) //by name
      };
      
      return query;
   }

   public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
   {
      if (string.IsNullOrEmpty(searchTerm)) return query;

      var lowerCaseSearchTearm = searchTerm.Trim().ToLower();

      return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTearm));
   }

   public static IQueryable<Product> Filter(this IQueryable<Product> query, string brand, string types)
   {
      var brandList = new List<string>();
      var typeList = new List<string>();

      if (!string.IsNullOrEmpty(brand)) brandList.AddRange(brand.ToLower().Split(",").ToList());
      if (!string.IsNullOrEmpty(types)) typeList.AddRange(types.ToLower().Split(",").ToList());

      query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
      query = query.Where(p => typeList.Count == 0 || typeList.Contains(p.Type.ToLower()));

      return query;
   }
}