using System;
using Newtonsoft.Json;

namespace Audition.Domain.Entities
{
    public class Product : IEquatable<Product>
    {
        public Product() {}

        public Product(Product toCopy)
        {
            if (toCopy == null)
            {
                return;
            }

            Brand = toCopy.Brand;
            FormattedRegularPrice = toCopy.FormattedRegularPrice;
            ImageUrl = toCopy.ImageUrl;
            Name = toCopy.Name;
            StyleId = toCopy.StyleId;
        }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("formatted_regular_price")]
        public string FormattedRegularPrice { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("style_id")]
        public int StyleId { get; set; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StyleId == other.StyleId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return StyleId;
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return String.Format("StyleId: {0}, Brand: {1}, Name: {2}, Price: {3}, ImageUrl: {4}",
                                 StyleId, Brand, Name, FormattedRegularPrice, ImageUrl);
        }
    }
}