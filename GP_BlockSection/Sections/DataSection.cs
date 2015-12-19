﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP_BlockSection.Sections
{
   // Подсчет площедей и типоак блок-секций
   public class DataSection
   {
      private SectionService _service;

      public List<SectionType> SectionTypes { get; private set; }      

      public int AverageFloors { get; private set; }// средняя этажность
      public double TotalAreaApart { get; private set; }
      public double TotalAreaBKFN { get; private set; }

      public DataSection(SectionService sectionService)
      {
         _service = sectionService;
      }

      /// <summary>
      /// Подсчет площадей блок-секций и типов
      /// </summary>
      public void Calc()
      {
         // Разбивка секций на типы и подсчет общей площади счекций одного типа
         Dictionary<string, SectionType> types = new Dictionary<string, SectionType>();
         foreach (var section in _service.Sections)
         {
            SectionType secType;
            string key = section.Name.ToUpper();
            if (!types.TryGetValue(key, out secType))
            {
               secType = new SectionType(section.Name, section.NumberFloor);
               types.Add(key, secType);
            }
            secType.AddSection(section);
         }
         SectionTypes = types.Values.ToList();

         // Подсчет общих значений для всех типов секций         
         AverageFloors = Convert.ToInt32(SectionTypes.Average(s => s.NumberFloor));
         TotalAreaApart = SectionTypes.Sum(s => s.AreaApartTotal);
         TotalAreaBKFN = SectionTypes.Sum(s => s.AreaBKFN);
      }
   }
}