using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;

namespace OrderManager.Tests.Domain.Entities
{
    public class OccurrenceEntityTests
    {
        [Fact]
        public void Ctor_WithOrderNumberAndTypeOccurrence_SetsPropertiesCorrectly()
        {
            int orderNumber = 123;
            ETypeOccurrenceEnum typeOccurrence = ETypeOccurrenceEnum.EmRotaDeEntrega;

            var occurrence = new OccurrenceEntity(orderNumber, typeOccurrence);

            Assert.Equal(orderNumber, occurrence.OrderNumber);
            Assert.Equal(typeOccurrence, occurrence.TypeOccurrence);
            Assert.False(occurrence.IndFinishing);
            Assert.True((DateTime.Now - occurrence.TimeOccurrence).TotalSeconds < 1); 
        }

        [Fact]
        public void Ctor_WithOrderNumberAndOccurrenceId_SetsPropertiesCorrectly()
        {
            int orderNumber = 4564;
            int occurrenceId = 1;

            var occurrence = new OccurrenceEntity(orderNumber, occurrenceId);

            Assert.Equal(orderNumber, occurrence.OrderNumber);
            Assert.Equal(occurrenceId, occurrence.Id);
        }

        [Fact]
        public void SetOccurrenceToFinishing_SetsIndFinishingTrueAndUpdatedAt()
        {
            var occurrence = new OccurrenceEntity(1021, ETypeOccurrenceEnum.EntregueComSucesso);

            // Act
            occurrence.SetOccurrenceToFinishing();

            // Assert
            Assert.True(occurrence.IndFinishing);
            Assert.NotNull(occurrence.UpdatedAt);
           
        }
    }
}
