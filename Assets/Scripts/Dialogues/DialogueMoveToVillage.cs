using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueMoveToVillage : MonoBehaviour, IDialogue
    {
        public LoadScreen loadScreen;
        public GameObject Interface;

        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator =
                CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist: true) as Character_Text;
            Character_Sprite MC =
                CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true) as Character_Sprite;
            Character_Sprite Coach =
                CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist: true) as Character_Sprite;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (Coach.isFacingLeft)
                Coach.Flip(immediate: true);
            Coach.SetPosition(Vector2.zero);
            if (MC.isFacingLeft)
                MC.Flip(immediate: true);
            MC.SetPosition(Vector2.zero);
// Сцена у кареты
            yield return narrator.Say(
                "Михаил выходит из дома, плотнее запахнув сюртук. Карета всё ещё ждёт у тротуара, но кучер теперь сидит ссутулившись, будто задремал. Лошадь беспокойно бьёт копытом по булыжнику.");

// Михаил будит кучера
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Стучит костяшками пальцев по дверце кареты");
            MC.Hightlight();
            yield return MC.Say("Просыпайся. Меняем маршрут — хутор за Чёрной речкой.");
            Coach.Hide();
// Кучер отвечает
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Вздрагивает, поправляет помятую шляпу. Глаза красные, будто не спал всю ночь");
            Coach.Hightlight();
            yield return Coach.Say(
                "Э-э-э... Ваше благородие, да там же — понижает голос — нечистое место. После заката и местные-то не ездят...");
            Coach.Hide();
// Реакция Михаила
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Холодно");
            MC.Hightlight();
            yield return MC.Say("Тем лучше. Значит, свидетель никуда не денется.");
            MC.Hide();
// Кучер продолжает
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Ёрзает на облучке, бросая взгляд на тёмное окно дома");
            Coach.Hightlight();
            yield return Coach.Say(
                "Да я ж не про людей... В прошлом месяце троих возил — двое назад пешком вернулись. Бормочет. А третий так и не нашёлся.");
            Coach.Hide();
// Михаил подкупает кучера
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Достаёт из кармана серебряный рубль, подбрасывает и ловит его");
            MC.Hightlight();
            yield return MC.Say("Тебе везти, не философствовать. Или рубль слишком лёгкий?");
            yield return narrator.Say("Монета сверкает в тусклом свете, и кучер, кряхтя, берётся за вожжи.");
            MC.Hide();
// Кучер соглашается
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Плюёт через левое плечо");
            Coach.Hightlight();
            yield return Coach.Say("Ладно... Только к реке подъедем — и шагом. А там как знаете.");
            Coach.Hide();
// Михаил садится в карету
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Забирается в карету, хлопая дверцей");
            MC.Hightlight();
            yield return MC.Say("Гони. И если услышишь крики — не останавливайся.");
            MC.Hide();
// Карета уезжает
            yield return narrator.Say(
                "Колёса скрипят, карета трогается, оставляя за собой дом с запертой дверью. Где-то впереди, за поворотом, уже сгущаются вечерние тени.");

// Завершение сцены
            QuestManager.Instance.SetQuestStage("mq001", 20);
            DialogueSystem.instance.CloseDialogue();
            Interface.SetActive(false);
            InputManager.Instance.ChangeCursorState(true);
            InputManager.Instance.ChangeCursorLock(false);
            loadScreen.Loading();
        }
    }
}