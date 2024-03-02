
using System.Threading.Tasks;

namespace TeamHorizon.DocuTranslator.Interfaces
{
    public interface IBatchDocuTranslationService
    {
        Task<bool> TranslateDocumentsAsync();
    }
}
