using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Shared
{
    public class DashBoardDTO
    {
        public int TotalLibros { get; set; }
        public int TotalLectores { get; set; }
        public int PrestamosRegistrados { get; set; }
        public int PrestamosPendientes { get; set; }
    }
}
