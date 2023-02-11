namespace ApiGRPCDiceyProject.ExtensionsMethods
{
    /// <summary>
    /// Méthodes d'extensions entre les DTOs et le Modèle.
    /// </summary>
    public static class ThrowDTOModelExtensions
    {
        #region méthodes pour un Dice
        /// <summary>
        /// Convertit un Throw (Model) en Throw (Dto).
        /// </summary>
        /// <param name="t"> Le lancer (Model) à convertir. </param>
        /// <returns> Le Throw (Dto) créé à partir du Model. </returns> 
        public static Throw ToDTO(this Api.Model.Throw.Throw t)
        {
            return new Throw() { ThrowId = t.Id.ToString(), IdDice = t.Dice.NbFaces,  Result = t.Result, ProfileId = t.ProfileId.ToString() };
        }
        #endregion

        #region méthodes pour une liste de Dice
        /// <summary>
        /// Convertit une liste de Throw (Model) en liste de Throw (Dto). 
        /// </summary>
        /// <param name="throws"> Liste de lancers (Model) à convertir. </param>
        /// <returns> Liste de Throw (Dto) créée à partir du modèle. </returns>
        public static List<Throw> ToDTO(this List<Api.Model.Throw.Throw> throws)
        {
            var dtoList = new List<Throw>();

            foreach (var modelThrow in throws) dtoList.Add(modelThrow.ToDTO());

            return dtoList;

        }
        #endregion
    }
}
