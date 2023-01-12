namespace ModelDTOExtensions
{
    /// <summary>
    /// Méthodes d'extensions entre les DTOs et le Modèle.
    /// </summary>
    public static class ModelDTOExtensions
    {
        #region méthodes pour un Dice
        /// <summary>
        /// Convertit un Dice (DTO) en Dice (Model).
        /// </summary>
        /// <param name="dice"> Le Dice (DTO) à convertir. </param>
        /// <returns> Le Dice (Model) créé à partir du DTO. </returns> 
        public static Api.Model.Dice ToModel(this Api.DTOs.Dice dice)
        {
            return new Api.Model.SimpleDice(dice.NbFaces);
        }

        /// <summary>
        /// Convertit un Dice (Model) en Dice (Dice).
        /// </summary>
        /// <param name="dice"> Le Dice (Model) à convertir. </param>
        /// <returns> Le Dice (DTO) créé à partir du DTO. </returns> 
        public static Api.DTOs.Dice ToDTO(this Api.Model.Dice dice)
        {
            return new Api.DTOs.Dice(dice.NbFaces);
        }
        #endregion

        #region méthodes pour une liste de Dice
        /// <summary>
        /// Convertit une liste de Dice (Model) en liste de Dice (DTO). 
        /// </summary>
        /// <param name="dice"> Liste de dice (Model) à convertir. </param>
        /// <returns> Liste de Dice (DTO) créée à partir du modèle. </returns>
        public static List<Api.DTOs.Dice> ToDTO(this List<Api.Model.Dice> dice)
        {
            var dtoList = new List<Api.DTOs.Dice>();

            foreach(Api.Model.Dice modelDice in dice) dtoList.Add(modelDice.ToDTO());

            return dtoList;
            
        }

        /// <summary>
        /// Convertit une liste de Dice (DTO) en liste de Dice (Model). 
        /// </summary>
        /// <param name="dice"> Liste de dice (DTO) à convertir. </param>
        /// <returns> Liste de Dice (Model) créée à partir du modèle. </returns>
        public static List<Api.Model.Dice> ToModel(this List<Api.DTOs.Dice> dice)
        {
            var modelList = new List<Api.Model.Dice>();

            foreach (Api.DTOs.Dice dtoDice in dice) modelList.Add(dtoDice.ToModel());

            return modelList;
        }
        #endregion
    }
}