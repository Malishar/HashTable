using LibraryClass;
using HashTable;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestTable
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddPoint_WhenCalled_AddsElementToHashTable()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard = new BankCard { Number = "123456789", Owner = "John Doe", Date = 20230501 };

            // Act
            hashTable.AddPoint(bankCard);

            // Assert
            Assert.IsTrue(hashTable.Contains(bankCard));
        }

        [TestMethod]
        public void RemovePoint_WhenElementExists_RemovesElementFromHashTable()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            hashTable.AddPoint(bankCard);

            // Act
            bool result = hashTable.RemoveData(bankCard);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(hashTable.Contains(bankCard));
        }


        [TestMethod]
        public void AddAfter_ReturnsFalse_WhenExistingDataNotInBucket()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard existingData = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            BankCard nonExistingData = new BankCard { Number = "999999999", Owner = "Nonexistent", Date = 2023 };
            BankCard newData = new BankCard { Number = "555555555", Owner = "Jack Johnson", Date = 2023 };
            hashTable.AddPoint(existingData);

            // Act
            bool result = hashTable.AddAfter(nonExistingData, newData);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(hashTable.Contains(newData));
        }


        [TestMethod]
        public void AddAfter_NonExistingData_ReturnsFalse()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard existingData = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2024 };
            hashTable.AddPoint(existingData);
            BankCard newData = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023  };

            // Act
            bool result = hashTable.AddAfter(new BankCard { Number = "999999999", Owner = "Nonexistent", Date = 2023 }, newData);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(hashTable.Contains(newData));
        }

        [TestMethod]
        public void RemoveData_RemovesExistingData_WhenDataIsTheOnlyOneInBucket()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard dataToRemove = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            hashTable.AddPoint(dataToRemove);

            // Act
            bool result = hashTable.RemoveData(dataToRemove);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(hashTable.Contains(dataToRemove));
        }

        [TestMethod]
        public void AddPoint_WhenCalledWithCollision_AddsElementToLinkedList()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(1); // Force collision
            BankCard bankCard1 = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };

            // Act
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);

            // Assert
            Assert.IsTrue(hashTable.Contains(bankCard1));
            Assert.IsTrue(hashTable.Contains(bankCard2));
        }

        [TestMethod]
        public void RemoveData_RemovesExistingData_WhenDataIsInLinkedList()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(1); // Force collision
            BankCard bankCard1 = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);

            // Act
            bool result = hashTable.RemoveData(bankCard2);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(hashTable.Contains(bankCard2));
            Assert.IsTrue(hashTable.Contains(bankCard1));
        }

        [TestMethod]
        public void PrintTable_PrintsAllElements()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard1 = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            hashTable.PrintTable();
            string result = sw.ToString();

            // Assert
            Assert.IsTrue(result.Contains(bankCard1.ToString()));
            Assert.IsTrue(result.Contains(bankCard2.ToString()));
        }

        [TestMethod]
        public void Contains_ReturnsFalse_ForEmptyTable()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };

            // Act
            bool result = hashTable.Contains(bankCard);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_ReturnsTrue_ForExistingElement()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            hashTable.AddPoint(bankCard);

            // Act
            bool result = hashTable.Contains(bankCard);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_ReturnsFalse_ForNonExistingElement()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard existingBankCard = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2024 };
            BankCard nonExistingBankCard = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };
            hashTable.AddPoint(existingBankCard);

            // Act
            bool result = hashTable.Contains(nonExistingBankCard);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveData_ReturnsFalse_ForNonExistingElement()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };

            // Act
            bool result = hashTable.RemoveData(bankCard);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Constructor_Parameterized()
        {
            // Arrange
            int data = 10;

            // Act
            Point<int> point = new Point<int>(data);

            // Assert
            Assert.AreEqual(data, point.Data);
            Assert.IsNull(point.Next);
            Assert.IsNull(point.Pred);
        }

        [TestMethod]
        public void ToString_Returns()
        {
            // Arrange
            int data = 10;
            Point<int> point = new Point<int>(data);

            // Act
            string result = point.ToString();

            // Assert
            Assert.AreEqual(data.ToString(), result);
        }

        [TestMethod]
        public void GetHashCode_ReturnsData()
        {
            // Arrange
            int data = 10;
            Point<int> point = new Point<int>(data);

            // Act
            int hashCode = point.GetHashCode();

            // Assert
            Assert.AreEqual(data.GetHashCode(), hashCode);
        }

        [TestMethod]
        public void NextProperty_Setter_SetsNextCorrectly()
        {
            // Arrange
            Point<int> point1 = new Point<int>();
            Point<int> point2 = new Point<int>();

            // Act
            point1.Next = point2;

            // Assert
            Assert.AreEqual(point2, point1.Next);
        }

        [TestMethod]
        public void PredProperty_Setter_SetsPredCorrectly()
        {
            // Arrange
            Point<int> point1 = new Point<int>();
            Point<int> point2 = new Point<int>();

            // Act
            point1.Pred = point2;

            // Assert
            Assert.AreEqual(point2, point1.Pred);
        }

        [TestMethod]
        public void GetIndex_DistributesIndicesCorrectly()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard1 = new BankCard { Number = "123456789", Owner = "John Doe", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "987654321", Owner = "Jane Smith", Date = 2023 };
            BankCard bankCard3 = new BankCard { Number = "555555555", Owner = "Jack Johnson", Date = 2023 };

            // Act
            int index1 = hashTable.GetIndex(bankCard1);
            int index2 = hashTable.GetIndex(bankCard2);
            int index3 = hashTable.GetIndex(bankCard3);

            // Assert
            Assert.IsTrue(index1 >= 0 && index1 < hashTable.Capacity, "Index1 is out of range.");
            Assert.IsTrue(index2 >= 0 && index2 < hashTable.Capacity, "Index2 is out of range.");
            Assert.IsTrue(index3 >= 0 && index3 < hashTable.Capacity, "Index3 is out of range.");
        }

        [TestMethod]
        public void AddMultipleElements_WithSameHashCode_AddsToSameBucket()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(1); // Force all elements to same bucket
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 2063 };
            BankCard bankCard3 = new BankCard { Number = "333", Owner = "Charlie", Date = 2025 };

            // Act
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);
            hashTable.AddPoint(bankCard3);

            // Assert
            Assert.IsTrue(hashTable.Contains(bankCard1));
            Assert.IsTrue(hashTable.Contains(bankCard2));
            Assert.IsTrue(hashTable.Contains(bankCard3));
        }

        [TestMethod]
        public void RemoveData_ChecksPredAndNextPointers_AfterRemoval()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 2063 };
            BankCard bankCard3 = new BankCard { Number = "333", Owner = "Charlie", Date = 2025 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);
            hashTable.AddPoint(bankCard3);

            // Act
            hashTable.RemoveData(bankCard2);

            // Assert
            Assert.IsTrue(hashTable.Contains(bankCard1));
            Assert.IsFalse(hashTable.Contains(bankCard2));
            Assert.IsTrue(hashTable.Contains(bankCard3));
            Assert.AreEqual(hashTable.GetIndex(bankCard1), hashTable.GetIndex(bankCard3));
        }

        [TestMethod]
        public void AddAfter_AddsElementAfterExistingElementInBucket()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(10);
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 2023 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 2063 };
            BankCard newBankCard = new BankCard { Number = "333", Owner = "Charlie", Date = 2025 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);

            // Act
            bool result = hashTable.AddAfter(bankCard1, newBankCard);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(hashTable.Contains(newBankCard));
        }

        [TestMethod]
        public void RemoveData_WithMultipleElementsInBucket_RemovesCorrectElement()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 20230101 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 20230101 };
            BankCard bankCard3 = new BankCard { Number = "333", Owner = "Charlie", Date = 20230101 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);
            hashTable.AddPoint(bankCard3);

            // Act
            hashTable.RemoveData(bankCard2);

            // Assert
            Assert.IsTrue(hashTable.Contains(bankCard1));
            Assert.IsFalse(hashTable.Contains(bankCard2));
            Assert.IsTrue(hashTable.Contains(bankCard3));
        }

        [TestMethod]
        public void Contains_AfterRemovingElement_ReturnsFalse()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard bankCard = new BankCard { Number = "111", Owner = "Alice", Date = 20230101 };
            hashTable.AddPoint(bankCard);
            hashTable.RemoveData(bankCard);

            // Act
            bool result = hashTable.Contains(bankCard);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveAllData_FromBucket_RemovesAllElements()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 20230101 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 20230101 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);

            // Act
            hashTable.RemoveData(bankCard1);
            hashTable.RemoveData(bankCard2);

            // Assert
            Assert.IsFalse(hashTable.Contains(bankCard1));
            Assert.IsFalse(hashTable.Contains(bankCard2));
        }

        [TestMethod]
        public void AddAfter_AddsToEndOfList_WhenExistingDataIsLast()
        {
            // Arrange
            MyHashTable<BankCard> hashTable = new MyHashTable<BankCard>(3);
            BankCard bankCard1 = new BankCard { Number = "111", Owner = "Alice", Date = 20230101 };
            BankCard bankCard2 = new BankCard { Number = "222", Owner = "Bob", Date = 20230101 };
            BankCard newBankCard = new BankCard { Number = "333", Owner = "Charlie", Date = 20230101 };
            hashTable.AddPoint(bankCard1);
            hashTable.AddPoint(bankCard2);

            // Act
            bool result = hashTable.AddAfter(bankCard2, newBankCard);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(hashTable.Contains(newBankCard));
        }
    }
}