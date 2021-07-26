using UnityEditor;
using System.Collections;

public class AdTimingEditorCoroutines
{
    readonly IEnumerator mRoutine;

    public static AdTimingEditorCoroutines StartEditorCoroutine( IEnumerator routine)
    {
        AdTimingEditorCoroutines coroutine = new AdTimingEditorCoroutines(routine);
        coroutine.start();
        return coroutine;
    }

    AdTimingEditorCoroutines(IEnumerator routine)
    {
        mRoutine = routine;
    }

    void start()
    {
        EditorApplication.update += update;
    }

    void update()
    {
        if(!mRoutine.MoveNext())
        {
            StopEditorCoroutine();
        }
    }

    public void StopEditorCoroutine()
    {
        EditorApplication.update -= update;
    }
}
