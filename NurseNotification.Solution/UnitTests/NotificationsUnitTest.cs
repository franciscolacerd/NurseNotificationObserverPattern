using FluentAssertions;
using Notification.Entities;
using Notification.Implementation;

namespace UnitTests
{
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
        public void Paciente_RemoveEnfermeiro_NaoDeveNotificarEnfermeiroRemovido()
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
}