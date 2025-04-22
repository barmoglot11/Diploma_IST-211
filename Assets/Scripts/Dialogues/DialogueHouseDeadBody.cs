using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueHouseDeadBody : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            // Описание склепа
            yield return narrator.Say("Сводчатый потолок, стены исписаны странными символами. В центре — каменная плита, где лежит Фёдор. Его глаза широко открыты, рот зашит грубой нитью. На груди — медная монета (пятак 1839 года).");
            yield return narrator.Say("Руки покойного неестественно скрючены, сложены на груди в жесте, напоминающем древний оккультный символ. Пальцы сломаны с методичной жестокостью - каждый сустав вывернут под невозможным углом, будто кто-то старательно складывал их по неведомому образцу.");
            yield return narrator.Say("На иссохшем лбу чернеет выжженный знак - точная копия того самого символа со страницы 47 дневника. Кожа вокруг обуглилась, образовав жуткий рельеф, словно метку, выжженную раскалённым железом.");
            yield return narrator.Say("Вокруг тела горят семь свечей, их синие язычки пламени застыли неподвижно, не колеблясь даже от сквозняка. Воск остаётся нетронутым - эти свечи горят, но не тают, будто питаются чем-то иным, нежели обычный огонь. Их мертвенный свет отбрасывает на стены пульсирующие тени, которые странным образом не совпадают с очертаниями предметов в комнате.");

// Действия Михаила
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Подносит руку к губам Фёдора — нить шевелится, будто живая");
            MC.Hightlight();
            yield return MC.Say("Mort... mais pas tranquille (Мёртв... но не спокоен).");

// Сверхъестественное явление
            yield return narrator.Say("Тень на стене вдруг отбрасывает не Фёдора, а что-то с слишком длинной шеей. Когда Михаил оборачивается — тень на месте.");

            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}