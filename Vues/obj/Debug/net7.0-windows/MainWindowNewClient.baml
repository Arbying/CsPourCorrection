   M S B A M L   `   `   `  ����?  ;Vues, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:VuesVues  5   	       Vues.MainWindowNewClient   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $localclr-namespace:Vues     5   	     � Title$  Nouveau client��5   	   $	��200��6    $	��400��6-    � WindowStartupLocation$ CenterScreen=�5   	   ���#8BC34A.��5      � $��5q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��5q�61   $��Numéro de carte:��6<   $��Center=�6T   ���#ECEFF1$
��Bold"�6�   �� 5   
   $��0��6   $��1��6   $��5q�6/   ��6:   �� *NumeroCarteFidelite+�� 5   
   $��1��6   $��0��6!   $��5q�61   $
��Nom:��6<   $��Center=�6H   ���#ECEFF1$
��Bold"�6x   �� 5   
   $��1��6   $��1��6   $��5q�6/   ��6:   �� *Nom+�� 5   
   $��2��6   $��0��6!   $��5q�61   $��Prénom:��6<   $��Center=�6K   ���#ECEFF1$
��Bold"�6{   �� 5   
   $��2��6   $��1��6   $��5q�6/   ��6:   �� *Prenom+�� 5    
   $��3��6   $��0��6!   $��5q�61   $��Date de naissance:��6<   $��Center=�6V   ���#ECEFF1$
��Bold"�6�   (  "System.Windows.Controls.DatePicker  5!   
   $��3��6   $��1��6"   $��5q�62      SelectedDate 6=   �� *DateNaissance+�� 5%   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   $��5q�6g   .+�5&      �� $��Enregistrer��6   $��5q�6+   ��66   �� *SaveClientCommand+�� 5'      $��Annuler��6   $��5q�6'   ��62   �� *CancelCommand1+5(      5)      5*      