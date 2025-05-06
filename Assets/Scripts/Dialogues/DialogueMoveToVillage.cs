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
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator =
                CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist: true) as Character_Text;
            Character_Sprite mc =
                CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true) as Character_Sprite;
            Character_Sprite coach =
                CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist: true) as Character_Sprite;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (coach.isFacingLeft)
                coach.Flip(immediate: true);
            coach.SetPosition(Vector2.zero);
            if (mc.isFacingLeft)
                mc.Flip(immediate: true);
            mc.SetPosition(Vector2.zero);
// Сцена у кареты
            yield return narrator.Say(
                "Михаил выходит из дома, плотнее запахнув сюртук. Карета всё ещё ждёт у тротуара, но кучер теперь сидит ссутулившись, будто задремал. Лошадь беспокойно бьёт копытом по булыжнику.");

// Михаил будит кучера
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Стучит костяшками пальцев по дверце кареты");
            mc.Hightlight();
            yield return mc.Say("Просыпайся. Меняем маршрут — хутор за Чёрной речкой.");
            coach.Hide();
// Кучер отвечает
            coach.Show();
            coach.SetSprite(mc.GetSprite($"{coach.name}-Scared"));
            coach.UnHightlight();
            yield return narrator.Say("Вздрагивает, поправляет помятую шляпу. Глаза красные, будто не спал всю ночь");
            coach.Hightlight();
            yield return coach.Say(
                "Э-э-э... Ваше благородие, да там же — понижает голос — нечистое место. После заката и местные-то не ездят...");
            coach.Hide();
// Реакция Михаила
            mc.Show();
            mc.Hightlight();
            yield return mc.Say("Тем лучше. Значит, свидетель никуда не денется.");
            mc.UnHightlight();
            yield return narrator.Say("Холодно раздается его ответ");
            mc.Hide();
// Кучер продолжает
            coach.Show();
            coach.SetSprite(mc.GetSprite($"{coach.name}-Shocked"));
            coach.UnHightlight();
            yield return narrator.Say("Ёрзает на облучке, бросая взгляд на тёмное окно дома");
            coach.Hightlight();
            yield return coach.Say(
                "Да я ж не про людей... В прошлом месяце троих возил — двое назад пешком вернулись. Бормочет. А третий так и не нашёлся.");
            coach.Hide();
// Михаил подкупает кучера
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Достаёт из кармана серебряный рубль, подбрасывает и ловит его");
            mc.Hightlight();
            yield return mc.Say("Тебе везти, не философствовать. Или рубль слишком лёгкий?");
            yield return narrator.Say("Монета сверкает в тусклом свете, и кучер, кряхтя, берётся за вожжи.");
            mc.Hide();
// Кучер соглашается
            coach.Show();
            coach.SetSprite(coach.GetSprite($"{coach.name}-Default"));
            coach.UnHightlight();
            yield return narrator.Say("Плюёт через левое плечо");
            coach.Hightlight();
            yield return coach.Say("Ладно... Только к реке подъедем — и шагом. А там как знаете.");
            coach.Hide();
// Михаил садится в карету
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Забирается в карету, хлопая дверцей");
            mc.Hightlight();
            yield return mc.Say("Гони. И если услышишь крики — не останавливайся.");
            mc.Hide();
// Карета уезжает
            yield return narrator.Say(
                "Колёса скрипят, карета трогается, оставляя за собой дом с запертой дверью. Где-то впереди, за поворотом, уже сгущаются вечерние тени.");

// Завершение сцены
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            QuestManager.Instance.SetQuestStage("mq001", 20);
            DialogueSystem.instance.CloseDialogue();
            Interface.SetActive(false);
            InputManager.Instance.ChangeCursorState(true);
            InputManager.Instance.ChangeCursorLock(false);
            loadScreen.Loading();
        }
    }
}