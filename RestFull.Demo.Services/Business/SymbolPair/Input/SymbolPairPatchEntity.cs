using System.ComponentModel.DataAnnotations;

namespace RestFullDemoAPI.Services.SymbolPair.Input
{
    public class SymbolPairPatchEntity
    {
        [Required]
        public bool IsEnable { get; set; } = false;
    }
}
