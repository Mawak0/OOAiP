namespace SpaceBattle.Lib;

public class Tree
{
    private readonly Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> treeObj = [];

    public Tree(List<(int, int, int, int)> fourElems)
    {
        var depth = fourElems.Select(f =>
            {
                AddFour(f);
                return 0;
            }
        ).Count();
    }

    public void AddFour((int, int, int, int) fourList)
    {
        var firstLevel = treeObj.TryGetValue(fourList.Item1, out var level1) ? level1 : (treeObj[fourList.Item1] = []);
        var secondLevel = firstLevel.TryGetValue(fourList.Item2, out var level2) ? level2 : (firstLevel[fourList.Item2] = []);
        var thirdLevel = secondLevel.TryGetValue(fourList.Item3, out var level3) ? level3 : (secondLevel[fourList.Item3] = []);
        thirdLevel.Add(fourList.Item4);
    }

    public bool Contains((int, int, int, int) fourList)
    {
        return treeObj.TryGetValue(fourList.Item1, out var l1) &&
            l1.TryGetValue(fourList.Item2, out var l2) &&
            l2.TryGetValue(fourList.Item3, out var l3) &&
            l3.Contains(fourList.Item4);
    }
}