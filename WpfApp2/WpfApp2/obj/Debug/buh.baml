   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	       WpfApp2.buh   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	   -   5          � Title$   ��5   	   $	��630��6)   $	��900��66    � WindowStartupLocation$ CenterScreen=�6B    � 
ResizeMode$ NoResize=�6g    � WindowStyle$
 None=�6}      GradientBrush#��[  6�   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dataBuh��5
   +   
�������$��	344,0,0,0q�6:   
�������$��3q�6a      CanUserSortColumns . 6�   �� 5   
   $��32,86,588,248q�6   .+�5      �� -   5        $��buhTitle��5   "   $��2q�6   $+��%Название отчётности��62   ���	#FFFFA082  textbox#��[ 6d   $
��Bold"�6�   $��23��6�   6�   �� 5      -   5         ��Name$ 
errorTitle��5   9   ���	#FFFF3C00$��10(�6+   $��23��6M   �� 5      
�������$
��Bold"�6'   $
��Left=�69   $��10(�6T   $��Убытки��6b   $	��177��6s   �� 5      -   5        $��PlanCB��5   "   $��2q�6   ���	#FFFFA082$
��Bold"�6G   6[   �� 5      -   5        $ 	errorPlan��5   9   ���	#FFFF3C00$��10(�6+   $��23��6L   �� 5      
�������$
��Bold"�6'   $
��Left=�69   $��10(�6T   $��Доходы��6b   $	��177��6s   �� 5      -   5        $��CreditCB��5   "   $��2q�6   ���	#FFFFA082$
��Bold"�6I   6]   �� 5      -   5        $ errorCredit��5   9   ���	#FFFF3C00$��10(�6+   $��23��6N   �� 5      
�������$
��Bold"�6'   $
��Left=�69   $��10(�6T   $<��6Дата формирования отчётности��6b   $	��177��6�   (  "System.Windows.Controls.DatePicker  5      -	   5        $��dateBuh��5   $   $��2q�6      DisplayDateStart$ 
2010/12/31X�63      DisplayDateEnd$ 
2023/12/31X�6Q   6o   �� 5      -
   5        $ 	errorDate��5   9   ���	#FFFF3C00$��10(�6+   $��23��6L   5      �� 5   
   $��38,378,594,140q�6   .+�5      �� -   5        $��7q�5        RoundCorner1#���  6    
�������$��10(�6Z   $��Добавить��6h   $	��100��6�   $��25��6�   �� 5      -   5        $��7q�5      #���  6    
�������$��10(�6Z   $��Обновить��6h   $	��100��6�   $��25��6�   �� 5      -   5        $��7q�5      #���  6    
�������$��10(�6Z   $��Удалить��6h   $	��100��6�   $��25��6�   5      �� 5   
   $��99,502,655,48q�6   .+�5      �� -   5        $��7q�5        RoundCorner#���  6    
�������$��10(�6Y   $��Экспорт��6g   $	��100��6�   $��25��6�   �� 5      -   5        $��7q�5      #���  6    
�������$��10(�6Y   $��
Назад��6g   $	��100��6�   $��25��6�   5       5!      5"      