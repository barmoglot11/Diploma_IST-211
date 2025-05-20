using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    [System.Serializable]

    public class BestiaryConfigData
    {
        public string MonsterName;
        public Material MonsterMaterial;
        [TextArea(minLines: 5, maxLines: 20)] public string MonsterDesc;

        public BestiaryConfigData Copy()
        {
            var result = new BestiaryConfigData
            {
                MonsterName = MonsterName,
                MonsterMaterial = MonsterMaterial,
                MonsterDesc = MonsterDesc,
            };
            return result;
        }

        public static BestiaryConfigData Default
        {
            get
            {
                BestiaryConfigData result = new BestiaryConfigData();

                result.MonsterName = "";
                
                result.MonsterDesc = "Обложка из потрескавшейся кожи будто дышит под пальцами, " +
                                     "а выжженный глаз в корнях кажется живым — его зрачок смещается, если смотреть слишком долго. " +
                                     "Внутри страницы пожелтели, покрыты каракулями, будто написанными не чернилами, а засохшей кровью. " +
                                     "Буквы дрожат, сливаются в бессвязные строки, но если присмотреться, в хаосе проглядывают очертания слов: " +
                                     "«глаза под веками», «не называйте имя», «корни в плоти» . " +
                                     "Иногда на полях проступают новые надписи — красные чернила, будто кто-то пишет прямо сейчас, пока вы читаете. " +
                                     "Одна фраза повторяется чаще других: «Не доверяй, что написано. Это оно говорит через меня» ." +
                                     "\n\n" +
                                     "Карты мест, где встречаются чудища, размыты восковыми пятнами, " +
                                     "будто их пытались скрыть. На одной из них — «Хутор за Черной речкой», но половина текста вычеркнута. " +
                                     "Под ней приписка: «Не ходи туда. Мы ошиблись» . " +
                                     "Рисунки существ похожи на людей с разорванными лицами, их глаза — точка и волна, будто они движутся. " +
                                     "Если читать вслух, голос становится чужим, а в конце предложения слышен смех, доносящийся из пустоты." +
                                     "\n\n" +
                                     "Самый странный эффект — текст оживает. Слова переползают с места на место, если отвлечься, а иногда исчезают вовсе. " +
                                     "Однажды на странице появилась запись: «Имя — его сила. Но имя — его ярмо. Назови — и он услышит». " +
                                     "Она написана вашим почерком."
                    ;
                result.MonsterMaterial = Resources.Load<Material>("Monster None");
                return result;
            }
        }
    }
}