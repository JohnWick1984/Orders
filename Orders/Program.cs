using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main()
    {
        // Создаем XML-файл с информацией о заказах
        CreateXmlFile("orders.xml");

        Console.WriteLine("XML файл успешно создан.");

        // Читаем информацию из XML-файла
        ReadXmlFile("orders.xml");
    }

    static void CreateXmlFile(string filePath)
    {
        // Создаем экземпляр XmlTextWriter для записи в XML-файл
        using (XmlTextWriter writer = new XmlTextWriter(filePath, null))
        {
            // Устанавливаем форматирование XML
            writer.Formatting = Formatting.Indented;

            // Начинаем запись XML-документа
            writer.WriteStartDocument();

            // Начинаем элемент "заказы"
            writer.WriteStartElement("заказы");

            // Добавляем информацию о заказах и товарах (пример)
            WriteOrder(writer, 1, "John Doe", "Oranges", 10, 100.00);
            WriteOrder(writer, 2, "Jane Doe", "Bananas", 5, 50.00);

            // Заканчиваем элемент "заказы"
            writer.WriteEndElement();

            // Заканчиваем запись XML-документа
            writer.WriteEndDocument();
        }
    }

    static void WriteOrder(XmlTextWriter writer, int orderId, string customer, string product, int quantity, double price)
    {
        // Начинаем элемент "заказ"
        writer.WriteStartElement("заказ");
        writer.WriteAttributeString("id", orderId.ToString());

        // Добавляем информацию о заказе
        writer.WriteElementString("клиент", customer);
        writer.WriteElementString("товар", product);
        writer.WriteElementString("количество", quantity.ToString());
        writer.WriteElementString("цена", price.ToString());

        // Заканчиваем элемент "заказ"
        writer.WriteEndElement();
    }
    static void ReadXmlFile(string filePath)
    {
        // Создаем экземпляр XmlDocument
        XmlDocument document = new XmlDocument();

        // Загружаем XML-файл в XmlDocument
        document.Load(filePath);

        // Получаем корневой элемент
        XmlElement root = document.DocumentElement;

        // Перебираем элементы "заказ" в корневом элементе
        foreach (XmlElement orderElement in root.SelectNodes("заказ"))
        {
            // Получаем атрибут "id" элемента "заказ"
            string orderId = orderElement.GetAttribute("id");

            // Получаем значения элементов внутри "заказ"
            string customer = orderElement.SelectSingleNode("клиент").InnerText;
            string product = orderElement.SelectSingleNode("товар").InnerText;
            string quantity = orderElement.SelectSingleNode("количество").InnerText;
            string price = orderElement.SelectSingleNode("цена").InnerText;

            // Выводим информацию на экран
            Console.WriteLine($"Заказ {orderId}: Клиент - {customer}, Товар - {product}, Количество - {quantity}, Цена - {price}");
        }
    }
}
