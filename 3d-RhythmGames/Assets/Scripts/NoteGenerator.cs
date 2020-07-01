using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour {
    public string FilePath;
    public GameObject NomalNotes;
    public Transform FirstPos;
    List<float> angle = new List<float>{-10.0f,-40.0f,-100.0f,-130.0f,-190.0f,-220.0f,-280.0f,-310.0f};

    //読み込むjsonに合わせたクラス
    [System.Serializable]
    public class InputJson{
        public string name;
        public int maxBlock;
        public float BPM;
        public int offset;
        public NotesDate[] notes;
    }

    [System.Serializable]
    public class NotesDate {
        public float LPB;
        public float num;
        public int block;
        public int type;
        public NotesDate[] notes;
    }

    string Title;
    float BPM;

    List<int> block = new List<int>(1024);
    List<GameObject> noteName = new List<GameObject>(1024);
    List<float> generatoTime = new List<float>(1024);

    float starttime;
    float now;
    float duration;
    float arrivalTime = 0.6f;
    int count = 0;

    void NoteDateLoad(NotesDate[] date)　{
        foreach(var note in date) {
            int type  = note.type;
            generatoTime.Add(note.num * ((60.0f / BPM) / note.LPB));
            block.Add(note.block);
            NoteDateLoad(note.notes);
            if(type == 1 ) {
                    noteName.Add(NomalNotes);
            } else {
                noteName.Add(NomalNotes);
            }
        }
    }

    void LoadJson() {
        string jsonText = Resources.Load<TextAsset>(FilePath).ToString();
        InputJson json = JsonUtility.FromJson<InputJson>(jsonText);
        Title = json.name;
        BPM = json.BPM;
        NoteDateLoad(json.notes);
    }

    void CheckNextNotes(float _duration){
        while(_duration - generatoTime[count] + arrivalTime > 0 && count < generatoTime.Count - 1.0f){
            SpawnNotes(count);
            Debug.Log(generatoTime[count]);
            count ++;
        }
    }

    void SpawnNotes(int i) {
        GameObject Note;
        Note = Instantiate(noteName[i],FirstPos.position,Quaternion.Euler(0,0,angle[block[i]]));
        Note.GetComponent<NomalNoteController>().setParameter(block[i]);
    }

    // Start is called before the first frame update
    void Start() {
        starttime = Time.time;
        LoadJson();
        //CheckNextNotesでindexエラーが起きるから最後尾に大きい負の数を追加
       generatoTime.Add(-111111);
    }

    // Update is called once per frame
    void Update() {
        now = Time.time;
        duration = now - starttime;
        CheckNextNotes(duration);
    }
}
