using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IFieldTypeRepository
    {
        IEnumerable<FieldDefinitionDto> GetAllFieldTypes();
        FieldType GetFieldTypeById(Guid fieldTypeId);
    }
}