using System;

namespace MVCInventario.Models
{
    public class Pager
    {
        public int iProductosTotales { get; private set; }
        public int iPaginaActual { get; private set; }
        public int iTamanoPagina { get; private set; }
        public int iPaginasTotales { get; private set; }
        public int iPaginaInicio { get; private set; }
        public int iPaginaFinal { get; private set; }

        public Pager()
        {

        }
        public Pager(int ProductosTotales, int Pagina, int TamanoPagina = 10)
        {
            int PaginasTotales = (int)Math.Ceiling((decimal)ProductosTotales / (decimal)TamanoPagina);
            int PaginaActual = Pagina;

            int PaginaInicio = PaginaActual - 5;
            int PaginaFinal = PaginaActual + 4;

            if (PaginaInicio <= 0)
            {
                PaginaFinal = PaginaFinal - (PaginaInicio - 1);
                PaginaInicio = 1;
            }
            if (PaginaFinal > PaginasTotales)
            {
                PaginaFinal = PaginasTotales;
                if (PaginaFinal > 10)
                {
                    PaginaInicio = PaginaFinal - 9;
                }
            }

            iProductosTotales = ProductosTotales;
            iPaginaActual = PaginaActual;
            iTamanoPagina = TamanoPagina;
            iPaginasTotales = PaginasTotales;
            iPaginaInicio = PaginaInicio;
            iPaginaFinal = PaginaFinal;

        }
    }
}

