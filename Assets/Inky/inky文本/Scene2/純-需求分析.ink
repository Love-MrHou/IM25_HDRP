VAR choiceA1_done = false
VAR choiceA2_done = false
VAR choiceA3_done = false
VAR choiceA4_done = false
VAR choiceA5_done = false

VAR choiceB1_done = false
VAR choiceB2_done = false
VAR choiceB3_done = false
VAR choiceB4_done = false
VAR choiceB5_done = false

VAR choiceC1_done = false
VAR choiceC2_done = false
VAR choiceC3_done = false
VAR choiceC4_done = false
VAR choiceC5_done = false
-> analysis
=== analysis ===
{(choiceA1_done && choiceA2_done && choiceA3_done && choiceA4_done && choiceA5_done) && (choiceB1_done && choiceB2_done && choiceB3_done && choiceB4_done && choiceB5_done) && (choiceC1_done && choiceC2_done && choiceC3_done && choiceC4_done && choiceC5_done) :
    -> summaryAndAnalysis
}
* GreenVital Foods的需求分析
    -> companyA
* Elegance Accessories的需求分析
    -> companyB
* EcoEssentials的需求分析
    -> companyC  


=== companyA ===
{choiceA1_done && choiceA2_done && choiceA3_done && choiceA4_done && choiceA5_done:
    * [進行其他公司分析]  -> analysis
}
*[您提到的年輕人群體具體是指哪個年齡段？] {not choiceA1_done} ->choiceA1
*[在預算方面，您希望如何分配這100,000美元？] {not choiceA2_done} ->choiceA2
*[在六個月內達成20%的品牌認知度提升，您覺得這個時程安排可行嗎？是否有過類似的經驗來支持這個目標？] {not choiceA3_done} ->choiceA3
*[您是否考慮過在這六個月內分階段評估廣告效果？如果效果不如預期，如何應對並調整策略？] {not choiceA4_done} ->choiceA4
*[您是否已經嘗試過其他的廣告平台？效果如何？] {not choiceA5_done} ->choiceA5

=== choiceA1 ===
我們主要針對18-35歲的年輕人，他們對健康食品有較高的興趣。#GreenVital Foods代表
* [返回]  -> returnFromChoiceA1

=== choiceA2 ===
我們計劃將預算分配為三個部分：30%用於廣告創意和製作，50%用於IG廣告投放，20%用於與影響者的合作。這樣的分配方式可以確保我們在創意、投放和推廣上都能有所保障。#GreenVital Foods代表
* [返回]-> returnFromChoiceA2

=== choiceA3 ===
六個月內提升20%品牌認知度有挑戰，但我們相信可行，通過密集曝光和精準定位加速這一過程，並密切監控隨時調整。#GreenVital Foods代表
* [返回]-> returnFromChoiceA3

=== choiceA4 ===
我們計劃中期和後期評估效果，若不如預期，將靈活調整策略，甚至加強影響者合作，以確保達成目標。#GreenVital Foods代表
* [返回] -> returnFromChoiceA4

=== choiceA5 ===
我們在Footbook和Aoogle Ads上投放過廣告，效果不錯，但年輕用戶參與有限，因此希望通過Instagrum吸引更多年輕群體。#GreenVital Foods代表
* [返回] -> returnFromChoiceA5

=== returnFromChoiceA1 ===
~ choiceA1_done = true
-> companyA

=== returnFromChoiceA2 ===
~ choiceA2_done = true
-> companyA

=== returnFromChoiceA3 ===
~ choiceA3_done = true
-> companyA 

=== returnFromChoiceA4 ===
~ choiceA4_done = true
-> companyA 

=== returnFromChoiceA5 ===
~ choiceA5_done = true
-> companyA 



=== companyB ===
{choiceB1_done && choiceB2_done && choiceB3_done && choiceB4_done && choiceB5_done:
    * [進行其他公司分析] -> analysis
}
*[您提到的目標客群是25-35歲的女性，能否具體描述這些客群的消費習慣或偏好？過去您是如何針對這群體進行營銷的？] {not choiceB1_done} ->choiceB1
*[150,000美元的預算分配上，您有沒有特別的考量？在廣告創意、投放和影響者合作之間，您希望如何分配這筆預算？] {not choiceB2_done} ->choiceB2
*[三個月內達成10%的銷售量提升是一個比較緊迫的目標，您覺得這個時程安排可行嗎？是否有過類似的經驗來支持這個目標？] {not choiceB3_done} ->choiceB3
*[您是否考慮過與高端影響者合作來進一步推廣？] {not choiceB4_done} ->choiceB4
*[您對於在廣告中強調產品的哪些特點有特別的要求？] {not choiceB5_done} ->choiceB5

=== choiceB1 ===
我們的目標客群是25-35歲女性，她們關注時尚和生活品質，對奢侈品有強烈購買意願。我們希望通過Instagrum更直接地與她們互動。#Elegance Accessories代表
* [返回]  -> returnFromChoiceB1

=== choiceB2 ===
我們計畫預算分配為30%創意製作，40%IG廣告，30%高端影響者合作，確保各環節達到最佳效果。#Elegance Accessories代表
* [返回]  -> returnFromChoiceB2

=== choiceB3 ===
三個月內提升10%銷售量有挑戰，但我們有信心通過精準投放和影響者合作實現目標，並根據市場反應調整策略。#Elegance Accessories代表
* [返回]  -> returnFromChoiceB3

=== choiceB4 ===
沒錯，我們認為與高端影響者合作是非常有效的推廣方式。他們的粉絲群體通常對品質和時尚有較高的要求，能夠有效提升我們的品牌價值。#Elegance Accessories代表
* [返回]  -> returnFromChoiceB4

=== choiceB5 ===
我們希望在廣告中強調產品的高品質材料和精湛工藝，讓消費者感受到產品的高端和奢華。#Elegance Accessories代表
* [返回]  -> returnFromChoiceB5

=== returnFromChoiceB1 ===
~ choiceB1_done = true
-> companyB

=== returnFromChoiceB2 ===
~ choiceB2_done = true
-> companyB

=== returnFromChoiceB3 ===
~ choiceB3_done = true
-> companyB

=== returnFromChoiceB4 ===
~ choiceB4_done = true
-> companyB

=== returnFromChoiceB5 ===
~ choiceB5_done = true
-> companyB



=== companyC ===
{choiceC1_done && choiceC2_done && choiceC3_done && choiceC4_done && choiceC5_done:
    * [進行其他公司分析] -> analysis
}
*[您提到的目標客群具體是哪一類消費者？他們在環保產品方面的消費習慣和偏好是什麼？] {not choiceC1_done} ->choiceC1
*[考慮到您目前的預算，您認為哪些推廣方式最能有效利用資金，達到您預期的效果？] {not choiceC2_done} ->choiceC2
*[在這五個月內，您對達成這些目標的時程是否有具體的計劃和階段性目標？] {not choiceC3_done} ->choiceC3
*[您對於提升品牌忠誠度有什麼具體的數據目標或預期嗎？] {not choiceC4_done} ->choiceC4
*[您有考慮過與環保主題的影響者或組織合作來推廣嗎？] {not choiceC5_done} ->choiceC5

=== choiceC1 ===
我們的目標客群是25-45歲的中高收入群體，注重環保和可持續生活方式，願意為品質和環保理念買單。#EcoEssentials代表
* [返回]  -> returnFromChoiceC1

=== choiceC2 ===
我們的70,000美元預算分配為30,000美元影響者合作，20,000美元社交媒體廣告，20,000美元內容創作和推廣活動。#EcoEssentials代表
* [返回]  -> returnFromChoiceC2

=== choiceC3 ===
五個月內，我們每月評估品牌忠誠度提升，設定階段性目標，以確保按時達成最終目標。#EcoEssentials代表
* [返回]  -> returnFromChoiceC3

=== choiceC4 ===
我們計劃五個月內提升品牌忠誠度20%，通過與現有客戶互動、提供優質服務和推出會員獎勵計劃達成。#EcoEssentials代表
* [返回]  -> returnFromChoiceC4

=== choiceC5 ===
我們認為與環保主題影響者合作是擴大品牌影響力的有效方式，這些影響者的價值觀與我們非常契合。#EcoEssentials代表
* [返回]  -> returnFromChoiceC5

=== returnFromChoiceC1 ===
~ choiceC1_done = true
-> companyC

=== returnFromChoiceC2 ===
~ choiceC2_done = true
-> companyC

=== returnFromChoiceC3 ===
~ choiceC3_done = true
-> companyC

=== returnFromChoiceC4 ===
~ choiceC4_done = true
-> companyC

=== returnFromChoiceC5 ===
~ choiceC5_done = true
-> companyC


=== summaryAndAnalysis ===
現在我們已經了解了各個廠商的需求和預算。讓我們來討論一下，選擇最符合我們公司預算要求的合作廠商。我們的預算是150,000美元，並且我們希望達成以下目標：提高品牌知名度、增加社交媒體互動以及推動銷售增長。#系統分析師
根據我們的預算和目標，哪一家公司最符合我們的要求？#市場部經理

*[GreenVital Foods，專注於健康食品，有100,000美元的預算，目標是六個月內提升品牌認知度20%] -> companyA1Chosen
*[Elegance Accessories，專注於高端時尚配件，有150,000美元的預算，目標是三個月內提高線上銷售量10%] -> companyB1Chosen
*[EcoEssentials，專注於環保產品，有70,000美元的預算，目標是五個月內提升品牌忠誠度20%] -> companyC1Chosen

=== companyA1Chosen ===
GreenVital Foods確實符合我們提升品牌認知度的目標，並且他們的預算也較為合理。不過，他們的預算僅為100,000美元，這可能限制我們在廣告創意和影響者合作上的靈活性，且目標時間較長，不利於短期內見效。#市場部經理
是的，A公司的預算較低，可能無法支持我們更廣泛的推廣需求。考慮到我們的150,000美元預算，我們或許需要考慮其他選項。#系統分析師

這個選擇似乎並不是最佳選擇，請重新選擇。
*[重新選擇] -> summaryAndAnalysis

=== companyB1Chosen ===
沒錯，Elegance Accessories的預算和目標符合我們需求，他們專注高端市場，與我們的品牌形象和受眾高度契合，有助於快速擴展市場影響力。#市場部經理
我同意。Elegance Accessories的預算和目標非常適合我們，他們的高端定位也能與我們品牌形象相輔相成，是個有潛力的合作夥伴。#系統分析師
-> summaryAndAnalysis

=== companyC1Chosen ===
EcoEssentials的環保產品有潛力，但他們的預算只有70,000美元，低於我們的目標，且提升品牌忠誠度需要較長時間，可能無法達到我們的短期銷售增長需求。#市場部經理
確實。雖然EcoEssentials在環保市場中有潛力，但考慮到我們的預算和短期目標，他們可能不太適合。我們應該選擇更符合我們需求的合作夥伴。#系統分析師

這個選擇似乎並不是最佳選擇，請重新選擇。
*[重新選擇] -> summaryAndAnalysis
