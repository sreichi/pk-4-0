using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FormFieldRepository : IFormFieldRepository
    {
        public IEnumerable<FormField> GetAllFormFieldsByForm(int formId)
        {
            throw new System.NotImplementedException();
        }
    }
}