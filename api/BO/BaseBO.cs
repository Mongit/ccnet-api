using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public interface IGuidId
    {
        Guid Id { get; set; }
    }

    public interface IFolio
    {
        int Folio { get; set; }
    }

    public class BaseBO : IGuidId
    {
        public BaseBO()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
