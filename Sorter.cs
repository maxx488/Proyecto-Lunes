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
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[left][1] < pivot)
                    {
                        left++;
                    }
                }
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[right][1] > pivot)
                    {
                        right--;
                    }
                }
                if (left < right)
                {
                    int[] temp = array[right];
                    array[right] = array[left];
                    array[left] = temp;
                }
                else
                {
                    return right;
                }
            }
            return Partition(array, left, right);
        }

        public void QuickSort(int[][] array, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(array, left, right);
                if (pivot > 1)
                {
                    QuickSort(array, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(array , pivot + 1, right);
                }
            }
        }
    }
}
