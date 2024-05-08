#if false        
int start = 0x0400, end = 0x04FF, charsPerLine = 16; 
Console.OutputEncoding = System.Text.Encoding.Unicode;
for(int i = start; i <= end; i++)
{
Console.Write(char.ConvertFromUtf32(i));
if((i - start + 1) % charsPerLine == 0)
Console.WriteLine();
}
#endif