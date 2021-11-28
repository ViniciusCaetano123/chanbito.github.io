using ProjetoEngenhariaSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoEngenhariaSoftware.Helpers
{
    public class Banco
    {
        private static List<PetClass> memoria;

        public static int insert(PetClass pet)
        {
            if(memoria == null)
            {
                memoria = new List<PetClass>();
            }
            pet.id = memoria.Count() + 1;
            memoria.Add(pet);
            return pet.id;
        }

        public static bool update(PetClass pet, int id)
        {
            try
            {
                memoria[id - 1] = pet;
                return true;
            }
            catch { }
            return false;

        }
        public static void delete(int id)
        {
                var a = memoria.Where(x => x.id == id).FirstOrDefault();
                memoria.Remove(a);
        }
        public static PetClass select(int ID = 0)
        {
            if(ID != 0)
            return memoria[ID - 1];
            return null;
        }

        public static List<PetClass> selectAll()
        {
            return memoria;
        }


    }
}