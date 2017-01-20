using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IStyleRepository
    {
        IEnumerable<StyleDto> GetAllStyles();
    }
}