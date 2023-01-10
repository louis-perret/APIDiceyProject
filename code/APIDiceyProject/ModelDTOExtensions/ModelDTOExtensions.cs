namespace ModelDTOExtensions
{
    public static class ModelDTOExtensions
    {
        public static Api.Model.Dice ToModel(this Api.DTOs.Dice dice)
        {
            return new Api.Model.SimpleDice(dice.NbFaces);
        }

        public static Api.DTOs.Dice ToDTO(this Api.Model.Dice dice)
        {
            return new Api.DTOs.Dice(dice.NbFaces);
        }
    }
}