﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IFormRepository
    {
        IEnumerable<FormListDto> GetAllForms();
        FormDetailDto GetFormById(Guid formId);
        FormListDto CreateNewForm(FormCreateDto formToCreate);
        IActionResult DeleteFormById(Guid formId);
        void Save();
    }
}