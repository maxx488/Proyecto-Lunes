using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Sorter
    {
        public int Partition(int[][] array, int left, int right)
        {
            int pivot;
            int aux = (left + right) / 2;
            pivot = array[aux][1];
            while (left <= right)
            {
                while (array[left][1] < pivot)
                {
                    left++;
                }
                while (array[right][1] > pivot)
                {
                    right--;
                }
                if (left <= right)
                {
                    int[] temp = array[right];
                    array[right] = array[left];
                    array[left] = temp;
                    left++;
                    right--;
                }
            }
            return left;
        }

        public void QuickSort(int[][] array, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(array, left, right);
                if (left < pivot - 1)
                {
                    QuickSort(array, left, pivot - 1);
                }
                if (pivot < right)
                {
                    QuickSort(array , pivot, right);
                }
            }
        }

        public int Partition(int[] array, int left, int right)
        {
            int pivot;
            int aux = (left + right) / 2;
            pivot = array[aux];
            while (left <= right)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if (left <= right)
                {
                    int temp = array[right];
                    array[right] = array[left];
                    array[left] = temp;
                    left++;
                    right--;
                }
            }
            return left;
        }

        public void QuickSort(int[] array, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(array, left, right);
                if (left < pivot - 1)
                {
                    QuickSort(array, left, pivot - 1);
                }
                if (pivot < right)
                {
                    QuickSort(array, pivot, right);
                }
            }
        }
    }
}
