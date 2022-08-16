namespace Dashboard.ViewModel
{
    public class ShopFilter : DtParameters
    {
        public int Id { get; set; }

        public int Fk_Branch { get; set; }
    }

    public class ShopGalleryFilter : DtParameters
    {
        public int Id { get; set; }

        public int Fk_Shop { get; set; }
    }
}
