using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class ListOfContracts<T> : IEnumerator<T> where T : Contract
    {
        private ListContract head;
        private ListContract head_pointer;

        public T Current { get { return head.content; } }

        object IEnumerator.Current => head;
        class ListContract
        {
            public T content;
            public ListContract next;
        }

        public void InsertSorted(T content)
        {
            ListContract p = head;
            if (head == null)
            {
                head = new ListContract { content = content };
            }
            else 
            {
                if (head.content.CompareTo(content) < 0)
                {
                    ListContract newContract = new ListContract { content = content, next = head };
                    head = newContract;
                }               
                else
                {
                    while (p.next != null && p.next.content.CompareTo(content) > 0)
                    {
                        p = p.next;
                    }
                    ListContract newContract = new ListContract { content = content, next = p.next };
                    p.next = newContract; 
                }
            }
        }

        public void RemoveFirst()
        {
            if (head != null) head = head.next;
        }

        public Contract GetFirst()
        {
            if (head != null)
            {
                Contract contract = head.content;
                return contract;
            }
            else
            {
                throw new ListIsEmptyException("ERROR: No quest left in the city");
            }
        }

        public int Count() //returns the length of the list
        {
            int count = 0;
            ListContract p = head;
            while (p != null)
            {
                count++;
                p = p.next;
            }
            return count;
        }

        public void Dispose()
        {

        }
        public bool MoveNext()
        {
            if (head_pointer == null)
            {
                // first call
                head_pointer = head;
                return true;
            }
            else if (head_pointer.next != null)
            {
                //n'th call
                head_pointer = head_pointer.next;
                return true;
            }
            else
            {
                //last call (end of the list)
                this.Reset();
                return false;
            }
        }
        public T this[int i]
        {
            get { return SearchItem(i); }
        }

        private T SearchItem(int index)
        {
            ListContract p = head;
            int count = 0;
            while (p != null && count < index)
            {
                p = p.next;
                count++;
            }

            if (p != null && count == index)
                return p.content;
            else
                return default;
        }
        public void Reset()
        {
            head_pointer = null;
        }
        public IEnumerator<T> GetEnumerator() {
            return this;
        }
    }
}
