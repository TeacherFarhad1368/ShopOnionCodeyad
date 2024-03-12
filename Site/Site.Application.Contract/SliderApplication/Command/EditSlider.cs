namespace Site.Application.Contract.SliderApplication.Command
{
    public class EditSlider : CreateSlider
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
