using System;
using Xunit;

namespace AOC2020.Tests
{
    public class PriorityQueueTests
    {
        [Fact]
        public void should_work_with_ascending()
        {
            var priorityQueue = new PriorityQueue<int>(i => i);
            priorityQueue.Enqueue(1);
            priorityQueue.Enqueue(2);
            priorityQueue.Enqueue(3);
            priorityQueue.Enqueue(4);
            priorityQueue.Enqueue(5);

            var best = priorityQueue.Dequeue();

            Assert.Equal(5, best);
        }

        [Fact]
        public void should_work_with_descending()
        {
            var priorityQueue = new PriorityQueue<int>(i => i);
            priorityQueue.Enqueue(5);
            priorityQueue.Enqueue(4);
            priorityQueue.Enqueue(3);
            priorityQueue.Enqueue(2);
            priorityQueue.Enqueue(1);

            var best = priorityQueue.Dequeue();

            Assert.Equal(5, best);
        }

        [Fact]
        public void should_work_with_priority_in_middle()
        {
            var priorityQueue = new PriorityQueue<int>(i => i);
            priorityQueue.Enqueue(4);
            priorityQueue.Enqueue(3);
            priorityQueue.Enqueue(5);
            priorityQueue.Enqueue(2);
            priorityQueue.Enqueue(1);

            var best = priorityQueue.Dequeue();

            Assert.Equal(5, best);
        }

        [Fact]
        public void should_work_with_multiple_top_priority()
        {
            var priorityQueue = new PriorityQueue<int>(i => i);
            priorityQueue.Enqueue(4);
            priorityQueue.Enqueue(3);
            priorityQueue.Enqueue(5);
            priorityQueue.Enqueue(5);
            priorityQueue.Enqueue(2);
            priorityQueue.Enqueue(1);

            var best = priorityQueue.Dequeue();

            Assert.Equal(5, best);
        }
    }
}
