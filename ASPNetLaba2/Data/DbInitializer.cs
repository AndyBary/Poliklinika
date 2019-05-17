using ASPNetLaba2.Models;
using System;
using System.Linq;

namespace ASPNetLaba2.Data 
{
    //простая начальная инициализация
    public static class DbInitializer
    {
        public static void Initialize(RegistraturaContext context)
        {
            context.Database.EnsureCreated();
            if (context.Zapis.Any())
            {
                return;
            }
            var zapises = new Zapis[]
            {
                new Zapis{ Zapis_date = "01/01/2019", Zapis_time = "10:00", PacientId=1, Kabinet=1, Url="http://zapis.su/dotnet"},
                new Zapis{ Zapis_date = "01/02/2019", Zapis_time = "10:45", PacientId=2, Kabinet=2, Url="http://zapis.su/webdev"},
                new Zapis{ Zapis_date = "01/03/2019", Zapis_time = "11:00", PacientId=3, Kabinet=3, Url="http://zapis.su/visualstudio"}
            };
            foreach (Zapis z in zapises)
            {
                context.Zapis.Add(z);
            }
            context.SaveChanges();

            if (context.Pacient.Any())
            {
                return;
            }
            var pacients = new Pacient[]
            {
                new Pacient {  Polis_number = 123456789, ZapisId=1, Pacient_FIO = "Малкин А.А.", Adres = "Профессиональная, 49" },
                new Pacient { Polis_number = 147258369, ZapisId=2, Pacient_FIO = "Старостин И.И.", Adres = "Красных Зорь, 16" },
                new Pacient { Polis_number = 789632145, ZapisId=3, Pacient_FIO = "Путин В.В.",  Adres = "Ивана Сусанина, 12" }

            };
            foreach (Pacient p in pacients)
            {
                context.Pacient.Add(p);
            }
            context.SaveChanges();

            if (context.Doctor.Any())
            {
                return;
            }
            var doctors = new Doctor[]
            {
                new Doctor { ZapisId=1, Doctor_FIO = "Гардеев М.П.", Doctor_Speciality = "Терапевт", Kabinet = 1},
                new Doctor { ZapisId=2, Doctor_FIO = "Булкин А.М.", Doctor_Speciality = "Хирург", Kabinet = 2},
                new Doctor { ZapisId=3, Doctor_FIO = "Федотов М.И", Doctor_Speciality = "Психиатр", Kabinet = 3}
            };
            foreach (Doctor d in doctors)
            {
                context.Doctor.Add(d);
            }
            context.SaveChanges();
        }
    }
}
