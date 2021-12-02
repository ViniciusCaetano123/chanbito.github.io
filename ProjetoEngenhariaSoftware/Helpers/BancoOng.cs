using ProjetoEngenhariaSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoEngenhariaSoftware.Helpers
{
    public class BancoOng
    {
        private static List<OngClass> memoria;

        public static int insert(OngClass pet)
        {
            if (memoria == null)
            {
                memoria = new List<OngClass>();
            }
            pet.id = memoria.Count() + 1;
            memoria.Add(pet);
            return pet.id;
        }

        internal static bool relacionar(int ID_Ong, int iDPet)
        {
            if(memoria[ID_Ong - 1].Pets.Where(x => x.id == iDPet).Count() > 0)
            {
                throw new Exception("Assossiação ja realizada");
            }
            else
            {
                memoria[ID_Ong - 1].Pets.Add(BancoPet.select(iDPet));
                return true;
            }
        }

        public static bool update(OngClass pet, int id)
        {
            try
            {
                memoria[id - 1] = pet;
                return true;
            }
            catch 
            {
                throw new KeyNotFoundException();
            }
            return false;

        }
        public static void delete(int id)
        {
            var a = memoria.Where(x => x.id == id).FirstOrDefault();
            memoria.Remove(a);
        }
        public static OngClass select(int ID = 0)
        {
            if (ID != 0)
                return memoria[ID - 1];
            return null;
        }
        public static List<OngClass> selectAll()
        {
            return memoria;
        }
    }
}