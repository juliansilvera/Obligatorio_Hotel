using System;
using HotelEstrellaDelMar.Models;

namespace HotelEstrellaDelMar
{
    class Program
    {
        static void Main(string[] args)
        {
            Hotel hotel = new Hotel();
            Menu menu = new Menu(hotel);
            menu.MenuPrincipal();
        }
    }
}
