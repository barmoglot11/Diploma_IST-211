using System.Collections;

namespace Interfaces
{
    public interface IDialogue
    {
        public void StartDialogue();
        
        public IEnumerator Dialogue();
    }
}