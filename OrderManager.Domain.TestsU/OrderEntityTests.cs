using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;
using System;
using Xunit;

namespace OrderManager.Tests.Domain.Entities
{
    public class OrderEntityTests
    {
        [Fact]
        public void Ctor_WithOrderNumberVOAndUserId_SetsPropertiesCorrectly()
        {
            var orderNumber = new OrderNumberVO(1234);
            int userId = 10;

            var order = new OrderEntity(orderNumber, userId);

            Assert.Equal(orderNumber, order.OrderNumber);
            Assert.Equal(userId, order.UserId);
            Assert.NotNull(order.Occurrences);
            Assert.Empty(order.Occurrences);
            Assert.False(order.IndDelivered);
            Assert.True((DateTime.Now - order.TimeOrder).TotalSeconds < 1);
        }

        [Fact]
        public void SetOrderStatusToDelivered_SetsIndDeliveredTrueAndUpdatedAt()
        {
            var order = new OrderEntity(new OrderNumberVO(5678), 5);

            order.SetOrderStatusToDelivered();

            Assert.True(order.IndDelivered);
            Assert.NotNull(order.UpdatedAt);
            Assert.True((DateTime.Now - order.UpdatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void AddOccurrenceToOrder_AddsOccurrenceAndUpdatesUpdatedAt()
        {
            // Arrange
            var order = new OrderEntity(new OrderNumberVO(9999), 2);
            var occurrence = new OccurrenceEntity(9999, ETypeOccurrenceEnum.ClienteAusente);

            // Act
            order.AddOccurrenceToOrder(occurrence);

            // Assert
            Assert.Single(order.Occurrences);
            Assert.Contains(occurrence, order.Occurrences);
            Assert.NotNull(order.UpdatedAt);
            Assert.True((DateTime.Now - order.UpdatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void DeleteOccurrenceFromOrder_RemovesOccurrenceAndUpdatesUpdatedAt()
        {
            // Arrange
            var order = new OrderEntity(new OrderNumberVO(8888), 3);
            var occurrence = new OccurrenceEntity(8888, ETypeOccurrenceEnum.AvariaNoProduto);
            order.AddOccurrenceToOrder(occurrence);

            // Act
            order.DeleteOccurrenceFromOrder(occurrence);

            // Assert
            Assert.Empty(order.Occurrences);
            Assert.NotNull(order.UpdatedAt);
            Assert.True((DateTime.Now - order.UpdatedAt).TotalSeconds < 1);
        }
    }
}
