/*== Serialización en C# ==*/
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Serialization {
    [Serializable]
    class Person {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age) {
            Name = name;
            Age = age;
        }
    }

    class SerializationExample {
        static void Main(string[] args) {
            Person person = new Person("Ana", 25);

            // Serialización binaria
            string binaryFile = "person.bin";
            using (FileStream fs = new FileStream(binaryFile, FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, person);
            }
            Console.WriteLine("Objeto serializado en binario.");

            // Deserialización binaria
            using (FileStream fs = new FileStream(binaryFile, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                Person deserializedPerson = (Person)formatter.Deserialize(fs);
                Console.WriteLine($"Deserializado: {deserializedPerson.Name}, {deserializedPerson.Age} años");
            }

            // Serialización JSON
            string jsonFile = "person.json";
            string json = JsonSerializer.Serialize(person);
            File.WriteAllText(jsonFile, json);
            Console.WriteLine("Objeto serializado en JSON.");

            // Deserialización JSON
            string jsonContent = File.ReadAllText(jsonFile);
            Person jsonPerson = JsonSerializer.Deserialize<Person>(jsonContent);
            Console.WriteLine($"Deserializado JSON: {jsonPerson.Name}, {jsonPerson.Age} años");

            // Limpiar archivos
            File.Delete(binaryFile);
            File.Delete(jsonFile);
            Console.WriteLine("Archivos eliminados.");
        }
    }
}
