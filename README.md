# Nurse Notification System - Observer Pattern - C#
Observer Pattern C# Implementation of a Nurse Notification System

In this example, the **Observer Pattern** is used to implement a nurse notification system. The system allows multiple nurses to observe changes in a patient's condition and receive real-time updates when critical events occur. 

The **Pacient** class acts as the observable, which represents the subject being monitored—typically a patient whose condition may change. As the patient's condition evolves, notifications need to be sent to the nurses responsible for their care. Each nurse is modeled as an observer, implementing logic to react to the notifications.

For instance, when a patient's condition changes to "critical," all registered nurses are immediately notified. Nurses who are no longer involved with the patient's care can be removed from the notification list, ensuring that only relevant staff receives critical updates. This pattern ensures that all interested parties are kept in sync with the patient's status without requiring direct coupling between the nurse and patient objects.

By decoupling the notification mechanism from the patient state logic, the system becomes more flexible and scalable. New types of observers (such as additional hospital staff or external monitoring systems) can be easily added without modifying the patient’s core functionality.

------

In software design and engineering, the observer pattern is a software design pattern in which an object, named the subject, maintains a list of its dependents, called observers, and notifies them automatically of any state changes, usually by calling one of their methods.

![Observer_Design_Pattern_UML](https://upload.wikimedia.org/wikipedia/commons/0/01/W3sDesign_Observer_Design_Pattern_UML.jpg)

https://en.wikipedia.org/wiki/Observer_pattern

------

## C# Implementation


### 1. Declare entities 

#### Conditions
```c#
    public struct Conditions
    {
        public const string Critical = "Critical";
        public const string Stable = "Stable";
    }
```
### 2. Declare contract interfaces
#### IPacient
```c#
    public interface IPacient
    {
        void AddNurse(INurse nurse);

        void RemoveNurse(INurse nurse);

        void NotifyNurses(string message);
    }
```

#### INurse
```c#
    public interface INurse
    {
        void Update(string message);
    }
```

### 3. Declare implementation
#### Pacient
```c#
    public class Pacient : IPacient
    {
        private List<INurse> nurses = new List<INurse>();

        public void AddNurse(INurse nurse)
        {
            this.nurses.Add(nurse);
        }

        public void NotifyNurses(string message)
        {
            foreach (var nurse in this.nurses)
            {
                nurse.Update(message);
            }
        }

        public void RemoveNurse(INurse nurse)
        {
            this.nurses.Remove(nurse);
        }

        public void ChangeCondition(string newCondition)
        {
            NotifyNurses($"The patient's condition changed to: {newCondition}");
        }
    }
```
#### Nurse
```c#
    public class Nurse : INurse
    {
        private string name;
        public List<string> Notifications { get; private set; }

        public Nurse(string name)
        {
            this.name = name;
            this.Notifications = new List<string>();
        }

        public void Update(string message)
        {
            this.Notifications.Add(message);
        }
    }
```

### 4. Unit test it (NUnit)
```c#
public class NotificationsUnitTest
    {
        private Pacient _pacient;
        private Nurse _firstNurse;
        private Nurse _secondNurse;

        [SetUp]
        public void Setup()
        {
            this._pacient = new Pacient();
            this._firstNurse = new Nurse("John Doe");
            this._secondNurse = new Nurse("Jane Doe");
        }

        [Test]
        public void Should_AddNurseAndNotify_ReturnTrue()
        {
            this._pacient.AddNurse(this._firstNurse);
            this._pacient.AddNurse(this._secondNurse);

            this._pacient.ChangeCondition(Conditions.Critical);

            this._firstNurse.Notifications.Should().ContainSingle()
                .Which.Should().Be($"The patient's condition changed to: {Conditions.Critical}");

            this._secondNurse.Notifications.Should().ContainSingle()
                .Which.Should().Be($"The patient's condition changed to: {Conditions.Critical}");
        }

        [Test]
        public void Should_RemoveNurseAndNotNotifyRemovedNurse_ReturnTrue()
        {
            this._pacient.AddNurse(this._firstNurse);
            this._pacient.AddNurse(this._secondNurse);
            this._pacient.RemoveNurse(this._firstNurse);

            this._pacient.ChangeCondition(Conditions.Stable);

            this._firstNurse.Notifications.Should().BeEmpty();

            this._secondNurse.Notifications.Should().ContainSingle()
                .Which.Should().Be($"The patient's condition changed to: {Conditions.Stable}");
        }
    }
```
