// See https://aka.ms/new-console-template for more information
using ABC_Optimization;

//statistics
//for (int j = 0; j < 10; j++)
//{
//    var beeColony = new BeeColony(3, 25);

//    for (int i = 0; i < 50; i++)
//    {
//        while (!beeColony.isGraphPainted())
//        {
//            beeColony.Iterate();
//        }

//        Console.Write(beeColony.ColorsCount + ", ");
//    }
//    Console.WriteLine();
//}
//detailed
//var beeColony = new BeeColony(3, 25);

//while (!beeColony.isGraphPainted())
//{
//    beeColony.Iterate();
//    Console.WriteLine(beeColony.ColorsCount + "\t" + beeColony.VisitedCount + "\t" + beeColony.paintedVertexes().Count);
//}

//test
var beeColony = new BeeColony(3, 25);

for (int i = 0; i < 50; i++)
{
    for(int j = 0; j < 20; j++)
    {
        beeColony.Iterate();
    }
    Console.WriteLine("\t" + beeColony.ColorsCount);
}