using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IFormRepository
    {
        IEnumerable<FormsDto> GetAllForms();
        SingleFormDto GetFormById(Guid formId);
        FormsDto CreateNewForm(FormCreateDto formToCreate);
        IActionResult DeleteFormById(Guid formId);
        void Save();
    }
}