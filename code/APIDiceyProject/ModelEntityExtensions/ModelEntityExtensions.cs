using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntityExtensions
{
    /// <summary>
    /// Méthodes d'extensions entre le Modèle et les entités.
    /// </summary>
    public static class ModelEntityExtensions
    {
        #region extensions Dice
        /// <summary>
        /// Convertit en Dice (Entity) en Dice (Model).
        /// </summary>
        /// <param name="dice"> Dice (Entity) à convertir. </param>
        /// <returns> Dice (Model) créé à partir de l'Entity. </returns>
        public static Api.Model.Dice ToModel(this Api.Entities.Dice dice)
        {
            return new Api.Model.SimpleDice(dice.NbFaces);
        }

        /// <summary>,M
        /// Convertit en Dice (Model) en Dice (Entity).
        /// </summary>
        /// <param name="dice"> Dice (Model) à convertir. </param>
        /// <returns> Dice (Entity) créé à partir de l'Entity. </returns>
        public static Api.Entities.Dice ToEntity(this Api.Model.Dice dice)
        {
            return new Api.Entities.Dice(dice.NbFaces);
        }
        #endregion

        #region extensions pour les listes.
        /// <summary>
        /// Convertit une list de Dice (Entity) en liste de Dice (Model).
        /// </summary>
        /// <param name="dices"> Liste de Dice (Entity) à convertir. </param>
        /// <returns> List de Dice (Model) créée à partir des Entity. </returns>
        public static List<Api.Model.Dice> ToModel(this List<Api.Entities.Dice> dices)
        {
            var modelList = new List<Api.Model.Dice>();

            foreach (Api.Entities.Dice entityDice in dices) modelList.Add(entityDice.ToModel());

            return modelList;
        }

        /// <summary>
        /// Convertit une list de Dice (Model) en liste de Dice (Entity).
        /// </summary>
        /// <param name="dices"> Liste de Dice (Model) à convertir. </param>
        /// <returns> List de Dice (Entity) créée à partir des Entity. </returns>
        public static List<Api.Entities.Dice> ToEntity(this List<Api.Model.Dice> dices)
        {
            var entityList = new List<Api.Entities.Dice>();

            foreach (Api.Model.Dice modelDice in dices) entityList.Add(modelDice.ToEntity());

            return entityList;
        }
        #endregion
    }
}
