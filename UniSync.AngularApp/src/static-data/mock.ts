import { Chat, ChatMessage } from 'src/app/pages/chat/chat.component';

export const recentChats: Chat[] = [
  {
    id: '1',
    imageUrl:
      'https://www.galda-verlag.de/wp-content/uploads/2018/06/top-7-books-that-changed-the-world.jpg',
    name: 'Group 1A1',
    lastMessage: "Don't forget about the meeting tomorrow!",
    unreadCount: 3,
    timestamp: '1 day ago',
    nrOfParticipants: 5
  },
  {
    id: '2',
    imageUrl:
      'https://gravatar.com/avatar/27205e5c51cb03f862138b22bcb5dc20f94a342e744ff6df1b8dc8af3c865109',
    name: 'Professor John',
    lastMessage: 'Office hours are changed to 2 PM on Wednesdays.',
    unreadCount: 1,
    timestamp: '3 days ago',
    nrOfParticipants: 2
  },
  {
    id: '3',
    imageUrl:
      'https://cdn.britannica.com/85/13085-050-C2E88389/Corpus-Christi-College-University-of-Cambridge-England.jpg',
    name: 'Year 1',
    lastMessage: 'Has anyone started working on the project yet?',
    unreadCount: 0,
    timestamp: '3 days ago',
    nrOfParticipants: 30
  }
];

export const recentChats2: Chat[] = [
  {
    id: '1',
    imageUrl:
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROnvZt4c3u_yXi7p_eUslwwZvj9yZk0L6hyw&s',
    name: 'Group 1A2',
    lastMessage: 'Anyone want to review heap sort implementation?',
    unreadCount: 2,
    timestamp: '3 hours ago',
    nrOfParticipants: 8
  },
  {
    id: '2',
    imageUrl: 'https://i.pravatar.cc/150?img=45',
    name: 'Dr. Sarah Johnson',
    lastMessage: 'Office hours extended to 5 PM this Thursday.',
    unreadCount: 1,
    timestamp: '1 day ago',
    nrOfParticipants: 2
  },
  {
    id: '3',
    imageUrl:
      'https://cdn.britannica.com/85/13085-050-C2E88389/Corpus-Christi-College-University-of-Cambridge-England.jpg',
    name: 'Year 1',
    lastMessage: 'Has anyone started working on the project yet?',
    unreadCount: 0,
    timestamp: '3 days ago',
    nrOfParticipants: 30
  }
];

export const convo1: ChatMessage[] = [
  {
    id: '1',
    senderId: 'student1',
    message: "Hey everyone, how's the project coming along?",
    messageTime: '2023-07-01T10:00:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
  },
  {
    id: '2',
    senderId: 'student2',
    message:
      "I've finished the research part. Working on the presentation now.",
    messageTime: '2023-07-01T10:05:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=2'
  },
  {
    id: '3',
    senderId: 'student3',
    message:
      "Great! I'm still working on the analysis. Should be done by tomorrow.",
    messageTime: '2023-07-01T10:10:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=3'
  },
  {
    id: '4',
    senderId: 'student1',
    message:
      "Sounds good. Don't forget about the meeting tomorrow at 3 PM to discuss our progress.",
    messageTime: '2023-07-01T10:15:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
  },
  {
    id: '5',
    senderId: 'student2',
    message:
      "Thanks for the reminder! Don't forget about the meeting, everyone.",
    messageTime: '2023-07-01T10:20:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=2'
  }
];

export const convo2: ChatMessage[] = [
  {
    id: '1',
    senderId: 'professor',
    message: "Hello! I hope you're doing well with your research project.",
    messageTime: '2023-07-02T09:00:00',
    senderPhotoUrl:
      'https://gravatar.com/avatar/27205e5c51cb03f862138b22bcb5dc20f94a342e744ff6df1b8dc8af3c865109'
  },
  {
    id: '2',
    senderId: 'cdc74746-5858-48f8-8c28-1ebc29406550',
    message:
      "Hello Professor! Yes, I'm making good progress. I have a question about the methodology section.",
    messageTime: '2023-07-02T09:05:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=5'
  },
  {
    id: '3',
    senderId: 'professor',
    message: "Of course, what's your question?",
    messageTime: '2023-07-02T09:10:00',
    senderPhotoUrl:
      'https://gravatar.com/avatar/27205e5c51cb03f862138b22bcb5dc20f94a342e744ff6df1b8dc8af3c865109'
  },
  {
    id: '4',
    senderId: 'cdc74746-5858-48f8-8c28-1ebc29406550',
    message:
      "I'm not sure if I should include the pilot study results in the main methodology section or as an appendix.",
    messageTime: '2023-07-02T09:15:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=5'
  },
  {
    id: '5',
    senderId: 'professor',
    message:
      "Great question. It's usually best to include a brief summary in the methodology and put the full results in an appendix. This keeps your main text focused while still providing all the necessary information.",
    messageTime: '2023-07-02T09:20:00',
    senderPhotoUrl:
      'https://gravatar.com/avatar/27205e5c51cb03f862138b22bcb5dc20f94a342e744ff6df1b8dc8af3c865109'
  }
];

export const convo3: ChatMessage[] = [
  {
    id: '1',
    senderId: 'student1',
    message:
      "Has anyone started working on the group project for Professor Johnson's class?",
    messageTime: '2023-07-03T14:00:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=6'
  },
  {
    id: '2',
    senderId: 'student2',
    message:
      "Not yet, but I've been gathering some resources. We should probably start soon.",
    messageTime: '2023-07-03T14:05:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=7'
  },
  {
    id: '3',
    senderId: 'student3',
    message:
      'I agree. Does anyone want to meet up this weekend to brainstorm ideas?',
    messageTime: '2023-07-03T14:10:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=8'
  },
  {
    id: '4',
    senderId: 'student4',
    message:
      'This weekend works for me. How about Saturday afternoon at the library?',
    messageTime: '2023-07-03T14:15:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=9'
  },
  {
    id: '5',
    senderId: 'student5',
    message:
      "Saturday afternoon is good for me too. Let's say 2 PM at the main study area?",
    messageTime: '2023-07-03T14:20:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=10'
  },
  {
    id: '6',
    senderId: 'student1',
    message:
      "Sounds perfect. I'll bring some snacks. Don't forget to bring your laptops and any research you've done so far!",
    messageTime: '2023-07-03T14:25:00',
    senderPhotoUrl: 'https://i.pravatar.cc/150?img=6'
  }
];

export const senderPhotoUrl =
  'https://cdn.prod.website-files.com/6365d860c7b7a7191055eb8a/65a751a180c6edec28086e13_Loki%20Bright-p-500.png';

export const receiverPhotoUrl =
  'https://cdn.prod.website-files.com/6365d860c7b7a7191055eb8a/65a74f4afec11d8c4c9a3dc5_Drew%20Cano-p-500.png';

export const receiverGuid = '7fef5c57-29de-4852-9729-979475c08417';

export const senderGuid = 'cdc74746-5858-48f8-8c28-1ebc29406550';
