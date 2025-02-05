using System.Collections.Generic;
using UnityEngine.Playables;

public interface ICustomNodeRenderer
{
    void DrawNode(in UnityEngine.Rect nodeRect, bool selected);
}

// Bridge between runtime and editor code: the graph created in runtime code can call GraphVisualizerClient.Show(...)
// and the EditorWindow will register itself with the client to display any available graph.
public class GraphVisualizerClient
{
    private static GraphVisualizerClient s_Instance;
    private List<PlayableGraph> m_Graphs = new List<PlayableGraph>();

    public static GraphVisualizerClient instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = new GraphVisualizerClient();
            return s_Instance;
        }
    }

    ~GraphVisualizerClient()
    {
        m_Graphs.Clear();
    }

    public static void Show(PlayableGraph graph)
    {
        if (!instance.m_Graphs.Contains(graph))
        {
            instance.m_Graphs.Add(graph);
        }
    }

    public static void Hide(PlayableGraph graph)
    {
        if (instance.m_Graphs.Contains(graph))
        {
            instance.m_Graphs.Remove(graph);
        }
    }

    public static void ClearGraphs()
    {
        instance.m_Graphs.Clear();
    }

    public static IEnumerable<PlayableGraph> GetGraphs()
    {
        return instance.m_Graphs;
    }
}
