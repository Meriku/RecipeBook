﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBookBL.Controller
{
    public class BaseController
    {

        public void Save<T>(List<T> item, CultureInfo culture)
        {
            var formatter = new BinaryFormatter();

            var fileName = $"{typeof(T).Name}_{culture.Name}.dat";

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }

        protected List<T> Load<T>(CultureInfo culture)
        {
            var formatter = new BinaryFormatter();
            var fileName = $"{typeof(T).Name}_{culture.Name}.dat";

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<T> items)
                {
                    return items;
                }
                else
                {
                    return new List<T>();
                }
            }
        }


    }
}
