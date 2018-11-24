using System.Collections.Generic;

namespace Funda1.Models
{
    public class PromoLabel
    {
        public bool HasPromotionLabel { get; set; }
        public IList<string> PromotionPhotos { get; set; }
        public IList<string> PromotionPhotosSecure { get; set; }
        public int PromotionType { get; set; }
        public int RibbonColor { get; set; }
        public object RibbonText { get; set; }
        public string Tagline { get; set; }
    }
}