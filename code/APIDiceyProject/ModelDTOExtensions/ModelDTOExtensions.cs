namespace ModelDTOExtensions
{
    /// <summary>
    /// Méthodes d'extensions entre les DTOs et le Modèle.
    /// </summary>
    public static class ModelDTOExtensions
    {
        #region méthodes pour un Dice
        /// <summary>
        /// Convertit un Dice (Model) en Dice (Model).
        /// </summary>
        /// <param name="dice"> Le Dice (Model) à convertir. </param>
        /// <returns> Le Dice (Model) créé à partir du Model. </returns> 
        public static Api.Model.Dice ToModel(this Api.DTOs.Dice dice)
        {
            return new Api.Model.SimpleDice(dice.NbFaces);
        }

        /// <summary>
        /// Convertit un Dice (Model) en Dice (Dice).
        /// </summary>
        /// <param name="dice"> Le Dice (Model) à convertir. </param>
        /// <returns> Le Dice (Model) créé à partir du Model. </returns> 
        public static Api.DTOs.Dice ToDTO(this Api.Model.Dice dice)
        {
            return new Api.DTOs.Dice(dice.NbFaces);
        }
        #endregion

        #region méthodes pour un Profile
        /// <summary>
        /// Convertit un Profile(DTOs) en Profile(Model)
        /// </summary>
        /// <param name="profile">Profile(DTOs) à convertir</param>
        /// <returns> Profile(Model) créé à partir de l'Entity</returns>
        public static Api.Model.Profile ToModel(this Api.DTOs.Profile profile)
        {
            return new Api.Model.SimpleProfile(profile.Id, profile.Name, profile.Surname);
        }

        /// <summary>
        /// Convertit un Profile(Model) en Profile(DTOs)
        /// </summary>
        /// <param name="profile">Profile(Model) à convertir</param>
        /// <returns>Profile(DTOs) créé à partir de l'Entity</returns>
        public static Api.DTOs.Profile ToDto(this Api.Model.Profile profile)
        {
            return new Api.DTOs.Profile(profile.Id, profile.Name, profile.Surname);
        }
        #endregion

        #region méthodes pour une liste de Dice
        /// <summary>
        /// Convertit une liste de Dice (Model) en liste de Dice (Model). 
        /// </summary>
        /// <param name="dice"> Liste de dice (Model) à convertir. </param>
        /// <returns> Liste de Dice (Model) créée à partir du modèle. </returns>
        public static List<Api.DTOs.Dice> ToDTO(this List<Api.Model.Dice> dice)
        {
            var dtoList = new List<Api.DTOs.Dice>();

            foreach(Api.Model.Dice modelDice in dice) dtoList.Add(modelDice.ToDTO());

            return dtoList;
            
        }

        /// <summary>
        /// Convertit une liste de Dice (Model) en liste de Dice (Model). 
        /// </summary>
        /// <param name="dice"> Liste de dice (Model) à convertir. </param>
        /// <returns> Liste de Dice (Model) créée à partir du modèle. </returns>
        public static List<Api.Model.Dice> ToModel(this List<Api.DTOs.Dice> dice)
        {
            var modelList = new List<Api.Model.Dice>();

            foreach (Api.DTOs.Dice dtoDice in dice) modelList.Add(dtoDice.ToModel());

            return modelList;
        }
        #endregion

        #region méthodes pour une liste de Profile
        /// <summary>
        /// Convertit une liste de Profile (Model) en liste de Profile (DTO). 
        /// </summary>
        /// <param name="profile"> Liste de Profile (Model) à convertir. </param>
        /// <returns> Liste de Profile (DTO) créée à partir du modèle. </returns>
        public static List<Api.DTOs.Profile> ToDTO(this List<Api.Model.Profile> profile)
        {
            var dtoList = new List<Api.DTOs.Profile>();

            foreach (Api.Model.Profile modelProfile in profile) dtoList.Add(modelProfile.ToDto());

            return dtoList;

        }

        /// <summary>
        /// Convertit une liste de Profile (DTO) en liste de Profile (Model). 
        /// </summary>
        /// <param name="profile"> Liste de Profile (DTO) à convertir. </param>
        /// <returns> Liste de Profile (Model) créée à partir du modèle. </returns>
        public static List<Api.Model.Profile> ToModel(this List<Api.DTOs.Profile> profile)
        {
            var modelList = new List<Api.Model.Profile>();

            foreach (Api.DTOs.Profile dtoProfile in profile) modelList.Add(dtoProfile.ToModel());

            return modelList;
        }
        #endregion
    }
}