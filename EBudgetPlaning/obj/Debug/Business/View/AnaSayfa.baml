   M S B A M L   `   `   `  ����I  EEBudgetPlaning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:EBudgetPlaning.Business.ViewEBudgetPlaning.Business.View  5   	   O,clr-namespace:EBudgetPlaning.Business.HelperEBudgetPlaning.Business.Helper  5   	   +    %EBudgetPlaning.Business.View.AnaSayfa   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:EBudgetPlaning.Business.View     5   	   ?helper,clr-namespace:EBudgetPlaning.Business.Helper     5   	     � Title$  AnaSayfa��5	   	   $��461.5��6   $��461.5��6)   $��1035.5��6;   $
��1200��6J    � WindowStartupLocation$ CenterScreen=�6Z    � 	Resources 5      3   -EBudgetPlaning.Business.Helper.ValueConverter   ConvertColor��%   &	          5   
   6    5      .��5      � $��
0,10,1,332q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��110* �6   .+�5   
   � $��0,0,545,-331q�6   $��2��6&   $	��420��67   $	��Top=�6D   .+�5      �� $��	0,151,0,0q�6e    �� SelectedItem 6   �� *selectedGelirModel+��6C   �� *	GelirList+ �� View 5      �� 5      .P�5      �� $��Gelir Kategori��6S    �� Width$
 Auto��6k    �� DisplayMemberBinding 6)   �� *
GelirAdi+5      �� 5      $��Gelir Miktarı��6W   $
 Auto��6n    6)   �� *GelirMiktari+5      �� 5      $��Gelir Tarihi��6V   $
 Auto��6l    6)   �� *GelirTarihi+6l   5      5       �� 5!      $��	GELIREKLE��69   $��GELİR EKLE��6W   $
��Left=�6l   $��	12,15,0,0q�6�   $	��Top=�6�   $	��146��6�   $
��Auto��6�   ��6   �� *MainPageCommand+6�   �� 5"      $��GELIRSIL��69   $��GELİR SİL��6U   $
��Left=�6i   $��	12,50,0,0q�6�   $	��Top=�6�   $	��146��6�   $
��Auto��6�   ��6   �� *MainPageCommand+6�   �� 5#      $��GELIRGUNCELLE��69   $��GELİR GÜNCELLE��6Z   $
��Left=�6s   $��	12,85,0,0q�6�   $	��Top=�6�   $	��146��6�   $
��Auto��6�   ��6   �� *MainPageCommand+6�   �� 5$      $��GELIRLERIGOR��69   $��BÜTÜN GELİRLER��6Y   $
��Left=�6r   $	��Top=�6�   $	��146��6�   $
��Auto��6�   $��
170,15,0,0q�6�   ��6   �� *MainPageCommand+6�   �� 5%      $��EXCELEAKTAR��69   $��EXCELE AKTAR��6X   $
��Left=�6o   $��
170,50,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6   �� *MainPageCommand+6�   �� 5&      $
��Left=�6   $��
12,130,0,0q�63   $
��Wrap=�6G   $��TOPLAM GELİR��6[   $	��Top=�6o   $��92��6�   $
��Bold"�6�   �� 5'      $
��Left=�6B   $��109,130,0,0q�6]   $
��Wrap=�6r   $	��Top=�6�   ��6   �� *ToplamGelir+ �� Mode$ TwoWay=�6�   �� 5(      $��GELIRANALİZİ��69   $��GELİR ANALİZİ��6Y   $
��Left=�6q   $��
326,15,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6   �� *MainPageCommand+6�   �� 5)      $��GELİRGRAFIK��69   $��GRAFİĞİ GÖR��6X   $
��Left=�6n   $��
326,50,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6   �� *MainPageCommand+6�   5,      � 5-   
   $��Right=�6   $��
0,0,0,-331q�6+   $��2��6?   $	��519��6P   .+�5.      �� $��
10,151,0,0q�6e   	 J� RenderTransformOrigin$	 
0.501,0.38&�6y    6   �� *selectedGiderModel+��6C   �� *	GiderList+ 5/      �� 50      .P�51      �� $��Gider Kategori��6S   $
 Auto��6k    6)   �� *
GiderAdi+6k   �� 52      $��Gider Miktarı��6W   $
 Auto��6n    6)   �� *GiderMiktari+6n   �� 53      $��Gider Tarihi��6V   $
 Auto��6l    6)   �� *GiderTarihi+6l   54   /   56      �� 57       # MaterialDesignRaisedLightButton#��[ 6   $��	GIDEREKLE��6r   $��GİDER EKLE��6�   $
��Left=�6�   $��	12,15,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6N   �� *MainPageCommand+6�   �� 58      #��[ 6   $��GIDERSIL��6r   $��GİDER SİL��6�   $
��Left=�6�   $��	12,50,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6N   �� *MainPageCommand+6�   �� 59      #��[ 6   $��GUNCELLE��6r   $��GİDER GÜNCELLE��6�   $
��Left=�6�   $��	12,85,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6N   �� *MainPageCommand+6�   �� 5:      #��[ 6   $��GIDERLERIGOR��6r   $��BÜTÜN GİDERLER��6�   $
��Left=�6�   $	��Top=�6�   $	��146��6�   $
��Auto��6�   $��
170,15,0,0q�6�   ��6N   �� *MainPageCommand+6�   �� 5;      #��[ 6   $��EXCELAL��6r   $��EXCELE AKTAR��6�   $
��Left=�6�   $��
170,50,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6N   �� *MainPageCommand+6�   �� 5<      $
��Left=�6   $��
12,130,0,0q�63   $
��Wrap=�6G   $��TOPLAM GİDER��6[   $	��Top=�6o   �� 5=      $
��Left=�6   $��115,130,0,0q�63   $
��Wrap=�6H   $	��Top=�6\   �� 6u   �6y   �� *ToplamGider+$ TwoWay=�6�   �� 5>      #��[ 6   $��GIDERANALIZI��6r   $��GİDER ANALİZİ��6�   $
��Left=�6�   $��
336,15,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6   ��6N   �� *MainPageCommand+6   �� 5?      #��[ 6   $��GIDERGRAFIGI��6r   $��GRAFİĞİ GÖR��6�   $
��Left=�6�   $��
336,50,0,0q�6�   $	��Top=�6�   $
��Auto��6�   $	��146��6�   ��6N   �� *MainPageCommand+6�   5@      5B      5C      