using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONExampleComplex : MonoBehaviour
{
    void Start()
    {
        /*SampleDataComplex sample = new SampleDataComplex();

        sample.name = "Mike";

        sample.address = new Address();
        sample.address.unit = 10;
        sample.address.road = "Johnson Street";
        sample.address.city = "New York City";

        sample.books = new Book[2];
        sample.books[0] = new Book();
        sample.books[0].bookAuthor = "John Bell";
        sample.books[0].bookName = "Intro to game dev";
        sample.books[0].isDigital = false;

        sample.books[0] = new Book();
        sample.books[0].bookAuthor = "Arthur Belly";
        sample.books[0].bookName = "New game dev Intro";
        sample.books[0].isDigital = false;*/

        //JSON serialization
        //string data = JsonUtility.ToJson(sample);

        //Debug.Log($"JSON = {data}");

        string filePath = Path.Combine(Application.dataPath, "JSONFolder/SampleComplexJSON.json");
        //File.WriteAllText(filePath, data);

        string json = File.ReadAllText(filePath);
        //Deserialization
        SampleDataComplex samplComplex = JsonUtility.FromJson<SampleDataComplex>(json);

        string name = samplComplex.name;
        string bookName = samplComplex.books[1].bookName;
        Debug.Log($"Name = {name}");
        Debug.Log($"Book Name = {bookName}");
    }
}
