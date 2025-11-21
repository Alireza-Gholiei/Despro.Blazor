namespace Despro.Blazor.Form.Components.ValueInputs
{
    public class ChipValidationArgs(List<string> chips, string currentChip, List<string> validationErrors)
    {
        /// <summary>
        /// The current chip value that could potentially be added
        /// </summary>
        public string CurrentChip { get; set; } = currentChip;

        /// <summary>
        /// A list of all chips
        /// </summary>
        public List<string> Chips { get; set; } = chips;

        /// <summary>
        /// The list of validation errors
        /// </summary>
        public List<string> ValidationErrors { get; set; } = validationErrors;
    }
}
